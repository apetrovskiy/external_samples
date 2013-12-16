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

namespace Typemock.Examples.CSharp.LiveObjects
{
    /// <summary>
    /// This test class demonstrates using live objects - test objects that have been instantiated normally, rather than
    /// as fake objects using Isolate.Fake.Instance(). These objects' behavior can still be modified by using WhenCalled methods
    /// and verified using Verify.
    /// 
    /// When a live object is used in WasCalled or its NonPublic counterparts, it will become a viable fake object. 
    /// 
    /// This behavior applies similarly to static methods which have not had their behavior defaults set up by 
    /// using Isolate.Fake.StaticMethods().
    /// </summary>
    [TestClass, Isolated(DesignMode.InterfaceOnly)]  // Note: Use Isolated to clean up after all tests in class
    public class LiveObjects
    {
        [TestMethod]
        public void CreateRealObject_FakeVoidMethod()
        {
            var dependency = new Dependency();

            Isolate.WhenCalled(() => dependency.CheckSecurity(null, null)).IgnoreCall();

            var classUnderTest =  new ClassUnderTest();
            var result = classUnderTest.Calculate(1, 2, dependency);
            Assert.AreEqual(3, result);
        }

       
        [TestMethod]
        public void VerifyMethods_OfRealObject()
        {
            var dependency = new Dependency();

            // Requires at least one WhenCalled, can be CallOriginal for Verify to work
            Isolate.WhenCalled(() => dependency.CheckSecurity(null, null)).IgnoreCall();

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.Calculate(1, 2, dependency);
            Isolate.Verify.WasCalledWithAnyArguments(()=>dependency.CheckSecurity(null,null));
        }  
    }
    
    //------------------
    // Classes under test
    // - Dependency: Class with Methods that need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------

    public class Dependency
    {
        public virtual void CheckSecurity(string name, string password)
        {
            throw new NotImplementedException();
        }
        public virtual int GetID()
        {
            return 10;
        }
    }

    public class ClassUnderTest
    {
        public int Calculate(int a, int b, Dependency dependency)
        {
            dependency.CheckSecurity("typemock", "rules");

            return a + b;
        }

        public int Calculate(int a, Dependency dependency)
        {
            return a + dependency.GetID();
        }
    }
}
