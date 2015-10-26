﻿using System.Collections.Generic;
using System.Linq;
using AppHarbor.Model;
using Apphbify.Data;

namespace Apphbify.Services
{
    public class DeploymentService : IDeploymentService
    {
        private readonly IApiService _Api;
        private readonly DataStore _Data;

        public DeploymentService(IApiService api, DataStore data)
        {
            _Api = api;
            _Data = data;
        }

        public DeploymentResult Deploy(string siteName, string regionId, App application, Dictionary<string, string> variables, out string slug)
        {
            bool variablesOk = true;
            bool addonsOk = true;
            slug = "";

            // Create the application at AppHarbor and store away the slug
            var createResult = _Api.CreateApplication(siteName, regionId);
            if (createResult.Status != CreateStatus.Created) return DeploymentResult.UnableToCreateApplication;
            slug = createResult.Id;

            // Attempt to disable precompilation. Not fatal if it fails.
            _Api.DisablePreCompilation(slug);

            // Configure file system access
            if (application.EnableFileSystem)
                _Api.EnableFileSystem(slug);

            // Set configuration variables
            foreach (var variable in variables.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)))
            {
                if (_Api.CreateConfigurationVariable(slug, variable.Key, variable.Value).Status != CreateStatus.Created)
                    variablesOk = false;
            }

            // Deploy the first code bundle
            if (!_Api.DeployBuild(slug, application.DownloadUrl)) return DeploymentResult.UnableToDeployCode;

            // Install addons
            foreach (var addon in application.Addons)
            {
                var a = _Data.GetAddonByKey(addon);
                if (a == null)
                {
                    addonsOk = false;
                    continue;
                }

                if (!_Api.EnableAddon(slug, a.Key, a.Plan))
                    addonsOk = false;
            }

            // Check for non-critical failures
            if (!variablesOk) return DeploymentResult.ErrorSettingVariables;
            if (!addonsOk) return DeploymentResult.ErrorInstallingAddons;

            return DeploymentResult.Success;
        }
    }

    public enum DeploymentResult
    {
        Success,
        UnableToCreateApplication,
        UnableToDeployCode,
        ErrorInstallingAddons,
        ErrorSettingVariables,
        Unspecified
    }
}