﻿using System.Collections.Generic;
using Apphbify.Data;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_addons_fail_to_install
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IDeploymentService> _Deploy;

        public When_addons_fail_to_install()
        {
            _Deploy = new Mock<IDeploymentService>(MockBehavior.Strict);
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug)).Returns(DeploymentResult.ErrorInstallingAddons);
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Deployment(_Deploy);
            });
            _Response = _Browser.Post("/Deploy/jabbr", with =>
            {
                with.FormValue("application_name", "JabbR Test");
                with.FormValue("region_id", "amazon-web-services::us-east-1");
            });
        }

        [Fact]
        public void It_should_redirect_to_sites()
        {
            _Response.ShouldHaveRedirectedTo("/Sites");
        }

        [Fact]
        public void It_should_have_an_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }

        [Fact]
        public void It_should_have_fired_the_deployment()
        {
            string slug;
            _Deploy.Verify(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug), Times.Once());
        }
    }
}