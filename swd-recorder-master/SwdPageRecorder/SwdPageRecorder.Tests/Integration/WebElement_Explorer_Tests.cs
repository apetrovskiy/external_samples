﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using SwdPageRecorder.WebDriver;

using FluentAssertions;

namespace SwdPageRecorder.Tests.Integration
{
    [TestClass]
    public class WebElement_Explorer_Tests : MyTest    
    {
        const int firstFrame = 0;
        const int secondFrame = 1;
        const int thirdFrame = 2;
        
        [TestMethod]
        public void WebElementExplorer_Test()
        {
            Helper.RunDefaultBrowser();
            Helper.LoadTestFile("page_with_frames.html");

            Helper.ToFrame(secondFrame);

            SwdBrowser.InjectVisualSearch();

            System.Threading.Thread.Sleep(1110*1000);


            //Helper.ClickId();
            
        }

    }
}
