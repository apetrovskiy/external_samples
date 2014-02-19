using System;
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


namespace SwdPageRecorder.UI
{
    public class JavascriptEditorTabPresenter : IPresenter<JavaScriptEditorView>
    {
        private JavaScriptEditorView view = null;


        public void InitWithView(JavaScriptEditorView view)
        {
            this.view = view;

            // Subscribe to WebDriverUtils events
            SwdBrowser.OnDriverStarted += InitControls;
            SwdBrowser.OnDriverClosed += InitControls;
            InitControls();

        }

        private void InitControls()
        {
            var driverIsRunning = SwdBrowser.IsWorking;
            if (driverIsRunning)
            {
                
            }
            else
            {
                
            }
        }
    }
}
