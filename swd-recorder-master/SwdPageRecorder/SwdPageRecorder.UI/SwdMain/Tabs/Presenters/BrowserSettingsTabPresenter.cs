﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using SwdPageRecorder.WebDriver;
using SwdPageRecorder.WebDriver.JsCommand;

using System.Xml;
using System.Xml.Linq;

using System.Windows.Forms;
using System.Diagnostics;

using System.Net;

using SwdPageRecorder.UI.FeaturesAndWorkarounds;

namespace SwdPageRecorder.UI
{
    public class BrowserSettingsTabPresenter : IPresenter<BrowserSettingsTabView>
    {
        private BrowserSettingsTabView view = null;
        private DesiredCapabilitiesData _desiredCapabilitiesdata = null;
        
        public void InitWithView(BrowserSettingsTabView view)
        {
            this.view = view;

            // Subscribe to WebDriverUtils events
            SwdBrowser.OnDriverStarted += InitControls;
            SwdBrowser.OnDriverClosed += InitControls;
            InitControls();

            view.SetStatus("Not started");
        }

        private void InitControls()
        {
            var driverIsRunning = SwdBrowser.IsWorking;
            if (driverIsRunning)
            {
                view.SetStatus("Running");
            }
            else
            {
                view.SetStatus("Not running");
            }
            
        }


        public bool wasBrowserStarted = false;
        public void StartNewBrowser(WebDriverOptions browserOptions, bool startSeleniumServerIfNotStarted)
        {
            if (wasBrowserStarted)
            {
                StopDriver();
            }
            else
            {
                if (startSeleniumServerIfNotStarted
                && !SeleniumServerProcess.IsRunning(browserOptions.RemoteUrl))
                {
                    Exception outException;
                    bool isOk = UIActions.PerformSlowOperation(
                                "Operation: Start local RemoteWebDriver Server",
                                () =>
                                {
                                    SeleniumServerProcess.Launch("start_selenium_server.bat");
                                    TestRemoteHub(browserOptions.RemoteUrl);
                                },
                                    out outException,
                                    null,
                                    TimeSpan.FromMinutes(2)
                                );

                    if (!isOk)
                    {
                        MyLog.Error("Failed to start local Selenium Server");
                        if (outException != null) throw outException;
                    }

                }
                
                StartDriver(browserOptions);
            }
        }

        public void StartDriver(WebDriverOptions browserOptions)
        {
            
            wasBrowserStarted = false;
            
            view.DisableDriverStartButton();
            
            Exception threadException;

            bool isSuccessful =  UIActions.PerformSlowOperation(
                                "Operation: Start new WebDriver instance",
                                () =>
                                {
                                    SwdBrowser.Initialize(browserOptions);
                                    wasBrowserStarted = true;
                                },
                                    out threadException,
                                    null,
                                    TimeSpan.FromMinutes(10)
                                );
            
            view.EnableDriverStartButton();

            if (isSuccessful)
            {
                SetDesiredCapabilities(browserOptions);
                view.DriverWasStarted();
            }
            else if (threadException != null) throw threadException;
        }

        private void SetDesiredCapabilities(WebDriverOptions browserOptions)
        {

            switch (browserOptions.BrowserName)
            {
                case WebDriverOptions.browser_Chrome:
                    _desiredCapabilitiesdata = new ChromeDesiredCapabilitiesData();
                    break;
                case WebDriverOptions.browser_InternetExplorer:
                    _desiredCapabilitiesdata = new IEDesiredCapabilitiesData();
                    break;
                default: 
                    _desiredCapabilitiesdata = new DesiredCapabilitiesData();
                    break;
            }
            
            
        }

        public void StopDriver()
        {
            view.DisableDriverStartButton();
            
            view.DriverIsStopping();

            Exception threadException;

            UIActions.PerformSlowOperation(
                        "Operation: Stop WebDriver instance",
                        () =>
                        {
                            SwdBrowser.CloseDriver();
                        },
                            out threadException,
                            null,
                            TimeSpan.FromMinutes(10)
                        );

            view.EnableDriverStartButton();
            
            wasBrowserStarted = false;
        }

        internal void InitDesiredCapabilities()
        {
            view.grdDesiredCapabilities.SelectedObject = _desiredCapabilitiesdata;
        }

        public static T? GetValueOrNull<T>(string valueAsString)
            where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }
        
        internal void LoadCapabilities()
        {
            var remoteDriver = (RemoteWebDriver)SwdBrowser.GetDriver();
            foreach (var prop in _desiredCapabilitiesdata.GetType().GetProperties())
            {
                string capabilityName = prop.Name.Replace("__", ".");

                if (!remoteDriver.Capabilities.HasCapability(capabilityName)) continue;

                object driverValue = remoteDriver.Capabilities.GetCapability(capabilityName);
                if (driverValue == null) continue;

                if (prop.PropertyType == typeof(bool?))
                {
                    bool? boolValue = GetValueOrNull<bool>(driverValue.ToString());
                    prop.SetValue(_desiredCapabilitiesdata, boolValue, null);
                }
                else if (prop.PropertyType == typeof(int?))
                {
                    int? intValue = GetValueOrNull<int>(driverValue.ToString());
                    prop.SetValue(_desiredCapabilitiesdata, intValue, null);
                }
                else
                {
                    prop.SetValue(_desiredCapabilitiesdata, driverValue.ToString(), null);
                }
            }
            InitDesiredCapabilities();
        }

        public bool TestRemoteHub(string url)
        {
            var client = new WebClient();
            string result = "OK";
            bool isOk = true;

            string response = "";
            try
            {
                response = client.DownloadString(url);
            }
            catch(Exception e)
            {
                isOk = false;
                result = "FAILED: " + e.Message;
            }
            view.SetTestResult(result, isOk);
            return isOk;
        }

        public void SetBrowserStartupSettings(WebDriverOptions browserOptions)
        {
                        
            view.SetBrowserStartupSettings(browserOptions);
        }

        public void ClickStart()
        {
            view.ClickOnStartButton();
        }
    }
}
