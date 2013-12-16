#region Copyright (c) 2004-2013, Typemock     http://www.typemock.com
/************************************************************************************
'
' Copyright © 2004-2013 Typemock Ltd
'
' This software is provided 'as-is', without any express or implied warranty. In no 
' event will the authors be held liable for any damages arising from the use of this 
' software.
' 
' Permission is granted to anyone to use this software for any purpose, including 
' commercial applications, and to alter it and redistribute it freely.
'
'***********************************************************************************/
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using System.Diagnostics;

namespace Typemock.Examples.CSharp.Basics
{
    /// <summary>
    /// Basic examples of how to use Typemock Isolator Syntax.
    /// The concept behind Arrange-Act-Assert is simple; we aspire for the test code to be divided into three stages:
    ///     - Arrange: here we set up our test objects and their behavior for the test duration
    ///     - Act: here we run the method under test using the test objects we created earlier
    ///     - Assert: here we verify that the outcome of running the test code with the test set up yielded the expected results    
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)] // Note: Use Isolated to clean up after the test
                                    // Faking static methods requires Pragmatic mode
    public class BasicUnitTesting
    {
        [TestMethod] 
        public void FakingDateTime()
        {
            // Arrange - Fake DateTime to think that it is 29th of Feb 2016
            Isolate.WhenCalled(() => DateTime.Now).WillReturn(new DateTime(2016, 2, 29));

            // Act 
            int result = MyCode.DoSomethingSpecialOnALeapYear();

            // Assert 
            Assert.AreEqual(100, result);
        }



        [TestMethod] 
        public void FakeAConcreteObjectExample()
        {
            // Arrange - Fake a Process, default is that all Members.ReturnRecursiveFakes 
            var fake = Isolate.Fake.Instance<Process>();
  
            Isolate.WhenCalled(() => fake.MainModule.Site.Name).WillReturn("Typemock rocks");

            // Act 
            var result = MyCode.IsMySiteNameTypemock(fake);

            // Assert 
            Assert.AreEqual(true, result);
        }
    }

    //------------------
    // Classes under test
    // - MyCode: Class that are Dependant on DateTime and Process and needs to be isolated from them to be unit tested
    //------------------

    public class MyCode
    {
        /// <summary>
        /// return 100 if we are on the 29th of February.
        /// </summary>
        public static int DoSomethingSpecialOnALeapYear()
        {
            if ((DateTime.Now.Month == 2) && (DateTime.Now.Day == 29))
                return 100;
            else
                return 0;
        }

        public static bool IsMySiteNameTypemock(Process process)
        {
            var name = process.MachineName;

            if (process.MainModule.Site.Name.StartsWith("Typemock"))
                return true;
            else
                return false;

        }
    }


   
}
