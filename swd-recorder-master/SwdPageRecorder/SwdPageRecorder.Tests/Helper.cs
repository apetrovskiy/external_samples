using System;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using SwdPageRecorder.WebDriver;

using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace SwdPageRecorder.Tests
{
    public static class Helper
    {


        // http://stackoverflow.com/a/283917/1126595

        static public string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        
        internal static void RunDefaultBrowser()
        {
            WebDriverOptions options = new WebDriverOptions()
            {
                BrowserName = WebDriverOptions.browser_Chrome,
                //BrowserName = WebDriverOptions.browser_PhantomJS,
            };

            SwdBrowser.Initialize(options);
        }



        internal static void LoadTestFile(string pageRelativePath)
        {
            string fullPath = Path.Combine(AssemblyDirectory(), "TestResource", pageRelativePath);
            Uri uri = new Uri(fullPath);

            string uriPath = uri.AbsoluteUri;

            SwdBrowser.GetDriver().Url = uriPath;
        }

        internal static void ClickId(string elementId)
        {
            var element = SwdBrowser.GetDriver().FindElement(By.Id(elementId));
            element.Click();
        }

        internal static void DumpArray(IEnumerable array)
        {
            foreach(object item in array)
            {
                Console.WriteLine(item.ToString());
            }
        }

        internal static object JS(string script)
        {
            var driver = SwdBrowser.GetDriver();
            IJavaScriptExecutor jsExec = driver as IJavaScriptExecutor;
            return jsExec.ExecuteScript(script);

        }

        internal static string JSToString(string script)
        {
            return Convert.ToString(JS(script));
        }

        internal static int JSToInt(string script)
        {
            return Convert.ToInt32(JS(script));
        }

        internal static bool JSToBool(string script)
        {
            return Convert.ToBoolean(JS(script));
        }



        internal static void JSToConsole(string script)
        {
            Console.WriteLine(JSToString(script));
        }

        internal static void ToFrame(int index)
        {
            var driver = SwdBrowser.GetDriver();
            driver.SwitchTo().Frame(index);
        }
    }
}
