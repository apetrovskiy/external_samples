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



namespace SwdPageRecorder.UI
{
    public class SwdMainPresenter
    {
        private SwdMainView view;
        public IWebDriver Driver { get { return SwdBrowser.GetDriver(); } }

        public Thread visualSearchWorker = null;

        const int VisualSearchQueryDelayMs = 777;


        public void InitView(SwdMainView view)
        {
            this.view = view;

            // Subscribe to WebDriverUtils events
            SwdBrowser.OnDriverStarted += InitControls;
            SwdBrowser.OnDriverClosed += InitControls;

            SwdBrowser.OnDriverStarted += InitSwitchToControls;

            InitControls();
        }

        private void InitSwitchToControls()
        {

            view.SetInitialRefreshMessageForSwitchToControls();
        }

        
        public void RefreshWindowsList()
        {
            Exception outException;
            bool isOk = false;

            isOk = UIActions.PerformSlowOperation(
                        "Operation: Refresh All Windows List",
                        () =>
                        {
                            BrowserWindow[] currentWindows = SwdBrowser.GetBrowserWindows();
                            string currentWindowHandle = SwdBrowser.GetCurrentWindowHandle();
                            view.UpdateBrowserWindowsList(currentWindows, currentWindowHandle);
                        },
                            out outException,
                            null,
                            TimeSpan.FromMinutes(1)
                        );

            if (!isOk)
            {
                MyLog.Error("Failed to refresh All Windows List");
                if (outException != null) throw outException;
            }
        }

        public void RefreshFramesList()
        {
            Exception outException;
            bool isOk = false;
            isOk = UIActions.PerformSlowOperation(
                        "Operation: Refresh All Frames List",
                        () =>
                        {
                            BrowserPageFrame rootFrame = SwdBrowser.GetPageFramesTree();
                            BrowserPageFrame[] currentPageFrames = rootFrame.ToList().ToArray();
                            SwdBrowser.SwitchToDefaultContent();
                            view.UpdatePageFramesList(currentPageFrames);
                        },
                            out outException,
                            null,
                            TimeSpan.FromMinutes(1)
                        );

            if (!isOk)
            {
                MyLog.Error("Failed to refresh All Frames List");
                if (outException != null) throw outException;
            }
        }


        
        public void RefreshSwitchToList()
        {
            if (SwdBrowser.IsWorking)
            {
                view.DisableSwitchToControls();
                RefreshWindowsList();
                RefreshFramesList();
                view.EnableSwitchToControls();
            }
            else
            {
                view.DisableSwitchToControls();
            }
        }


        internal void SetBrowserUrl(string browserUrl)
        {
            Driver.Navigate().GoToUrl(browserUrl);
        }



        public void ProcessCommands()
        {
            var command = SwdBrowser.GetNextCommand();
            if (command is GetXPathFromElement)
            {
                var getXPathCommand = command as GetXPathFromElement;
                view.UpdateVisualSearchResult(getXPathCommand.XPathValue);
            }
            else if (command is AddElement)
            {
                var addElementCommand = command as AddElement;

                var element = new WebElementDefinition()
                {
                    Name = addElementCommand.ElementCodeName,
                    HowToSearch = LocatorSearchMethod.XPath,
                    Locator = addElementCommand.ElementXPath,
                };
                bool addNew = true;
                Presenters.SelectorsEditPresenter.UpdateWebElementWithAdditionalProperties(element);
                Presenters.PageObjectDefinitionPresenter.UpdatePageDefinition(element, addNew);
            }
        }

        bool webElementExplorerStarted = false;
        bool webElementExplorerThreadPaused = false;

        public void ResumeWebElementExplorerProcessing()
        {
            MyLog.Write("ResumeWebElementExplorerProcessing");
            webElementExplorerThreadPaused = false;
        }

        public void PauseWebElementExplorerProcessing()
        {
            MyLog.Write("PauseWebElementExplorerProcessing");
            webElementExplorerThreadPaused = true;
        }

        public void VisualSearch_UpdateSearchResult()
        {
            try
            {
                MyLog.Write("VisualSearch_UpdateSearchResult: Started");
                while (webElementExplorerStarted == true)
                {
                    if (!webElementExplorerThreadPaused)
                    {
                        try
                        {
                            if (!SwdBrowser.IsVisualSearchScriptInjected())
                            {
                                MyLog.Write("VisualSearch_UpdateSearchResult: Found the Visual search is not injected. Injecting");
                                SwdBrowser.InjectVisualSearch();
                            }

                            ProcessCommands();
                        }
                        catch (Exception e)
                        {
                            StopVisualSearch();
                            MyLog.Exception(e);
                        }
                    }
                    Thread.Sleep(VisualSearchQueryDelayMs);
                }
            }
            finally
            {
                StopVisualSearch();
                MyLog.Write("VisualSearch_UpdateSearchResult: Finished");
            }

        }

        internal void StopVisualSearch()
        {
            view.VisualSearchStopped();
            webElementExplorerStarted = false;
        }

        internal void StartVisualSearch()
        {
            SwdBrowser.InjectVisualSearch();
            if (visualSearchWorker != null)
            {
                visualSearchWorker.Abort();
                visualSearchWorker = null;
            }

            webElementExplorerStarted = true;

            visualSearchWorker = new Thread(VisualSearch_UpdateSearchResult);
            visualSearchWorker.IsBackground = true;
            visualSearchWorker.Start();

            while (!visualSearchWorker.IsAlive)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }

            view.VisuaSearchStarted();

        }


        internal void ChangeVisualSearchRunningState()
        {
            if (webElementExplorerStarted)
            {
                StopVisualSearch();
            }
            else
            {
                StartVisualSearch();
            }
        }

        internal void InitControls()
        {
            var shouldControlBeEnabled = SwdBrowser.IsWorking;
            view.SetDriverDependingControlsEnabled(shouldControlBeEnabled);

        }

        public void DisplayLoadingIndicator(bool showLoading)
        {
            if (showLoading)
            {
                view.ShowGlobalLoading();
            }
            else
            {
                view.HideGlobalLoading();
            }
        }

        internal void SwitchToFrame(BrowserPageFrame frame)
        {
            PauseWebElementExplorerProcessing();
            try
            {
                SwdBrowser.DestroyVisualSearch();
                SwdBrowser.GoToFrame(frame);
                MyLog.Write("FRAME: Switched to frame with Index= " + frame.Index + "; and Full Name:" + frame.ToString());
            }
            finally
            {
                ResumeWebElementExplorerProcessing();
            }
        }



        internal void SwitchToWindow(BrowserWindow window)
        {
            PauseWebElementExplorerProcessing();
            view.DisableSwitchToControls();
            try 
            {
                SwdBrowser.GotoWindow(window);
                MyLog.Write("WINDOW: Switched to window/popup with WinID= "
                            + window.WindowHandle + "; and Title:" + window.Title);
                RefreshFramesList();
            }
            finally 
            {
                ResumeWebElementExplorerProcessing();
                view.EnableSwitchToControls();
            }
        }
    }
}
