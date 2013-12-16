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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Typemock.Examples.CSharp.RefOutArguments
{
    /// <summary>
    /// This test class shows different ways of controlling the behavior of ref and out arguments
    /// To set the return value of a ref or out arguement, set them before calling the WhenCalled API.
    /// </summary>
    [TestClass, Isolated] // Note: Use Isolated to clean up after all tests in class
    public class RefOutArguments
    {
        [TestMethod]
        public void ReturnValuesInRefArgument()
        {
            string outStr = "typemock";
            List<int> outList = new List<int>() { 100 };

            var realDependency = new Dependency();
            Isolate.WhenCalled(() => realDependency.SomeMethod(ref outStr, out outList))
                .IgnoreCall();

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.GetString(realDependency);
            Assert.AreEqual("typemock1", result);
        }

        [TestMethod]
        public void VerifyRefArguments()
        {
            string outStr = "typemock";

            var fake = new Dependency();
            Isolate.WhenCalled(() => fake.SomeMethod(ref outStr)).IgnoreCall();

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.UseRef(fake);

            string inputShouldbe = "unit testing";
            Isolate.Verify.WasCalledWithExactArguments(() => fake.SomeMethod(ref inputShouldbe));
        }
    }

    //------------------
    // Classes under test
    // - Dependency: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------
    
    public class Dependency
    {
        public virtual void SomeMethod(ref string name, out List<int> list)
        {
            throw new NotImplementedException();
        }
        public virtual void SomeMethod(ref string name)
        {
            throw new NotImplementedException();
        }
    }

    public class ClassUnderTest
    {
        public string GetString(Dependency dependency)
        {
            var name = "unit testing";
            List<int> list;
            dependency.SomeMethod(ref name, out list);

            return name + list.Count.ToString();
        }

        public string UseRef(Dependency dependency)
        {
            var name = "unit testing";
            dependency.SomeMethod(ref name);

            return name ;
        }
    }
}
