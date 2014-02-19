using System;

using System.IO;

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
    public class FullHtmlSourceTabPresenter: IPresenter<FullHtmlSourceTabView>
    {
        private FullHtmlSourceTabView view;

        public void InitWithView(FullHtmlSourceTabView view)
        {
            this.view = view;

            // Subscribe to WebDriverUtils events
            SwdBrowser.OnDriverStarted += InitControls;
            SwdBrowser.OnDriverClosed += InitControls;
            InitControls();
        }

        private void InitControls()
        {
            var shouldControlBeEnabled = SwdBrowser.IsWorking;
            view.btnGetHtmlSource.Enabled = shouldControlBeEnabled;
            view.txtHtmlPageSource.Enabled = shouldControlBeEnabled;
        }

        internal void DisplayHtmlPageSource()
        {
            string htmlSource = SwdBrowser.GetHtml();
            view.FillHtmlCodeBox(htmlSource);
        }


        internal void SendContentToBrowser(string htmlContent)
        {
            byte[] bytes = new byte[htmlContent.Length * sizeof(char)];
            Buffer.BlockCopy(htmlContent.ToCharArray(), 0, bytes, 0, bytes.Length);
            string base64 = Convert.ToBase64String(bytes);
            string dataUri = "data:text/html;base64," + base64;

            SwdBrowser.Url = dataUri;
        }

        internal void TidyHtml(string htmlContent)
        {

            throw new NotImplementedException();
        }
    }
}
