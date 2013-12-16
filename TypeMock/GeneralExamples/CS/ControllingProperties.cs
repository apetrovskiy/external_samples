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

namespace Typemock.Examples.CSharp.ControllingProperties
{
    /// <summary>
    /// This test class shows different ways of controlling the behavior of fake properties
    /// The supported behaviors are:
    /// <list type="bullet">
    ///     <item>Using WhenCalled - Like other methods</item>
    ///     <item>Using True Properties - To fake property to act like an auto-property</item>
    /// </list>
    /// </summary>
    [TestClass, Isolated]// Note: Use Isolated to clean up after all tests in class
    public class ControllingProperties
    {
        [TestMethod]
        public void FakePropertyGetter_UsingWhenCalled()
        {
            var fakeDependency = new Dependency();
           
            Isolate.WhenCalled(() => fakeDependency.Number).WillReturn(5);

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.SimpleCalculation(2, fakeDependency);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void FakePropertyGetter_UsingTrueProperty()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();
            fakeDependency.Number=5;

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.SimpleCalculation(2, fakeDependency);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void FakePropertySetter_UsingWhenCalled()
        {
            var fakeDependency = new Dependency();

            int countCalls = 0;
            Isolate.WhenCalled(() => fakeDependency.Number=5).DoInstead(c=>countCalls++);

            var result = new ClassUnderTest().SimpleCalculation(2, fakeDependency);

            Assert.AreEqual(1, countCalls);
        }
    }


    //------------------
    // Classes under test
    // - Dependency: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------

    public class Dependency
    {
        private int _number;
        public virtual int Number
        {
            get { return _number; }
            set
            {
                CheckSecurity();
                _number = value; 
            }
        }
        public void CheckSecurity()
        {
            throw new NotImplementedException();
        }
    }

    public class ClassUnderTest
    {
        public int SimpleCalculation(int a, Dependency dependency)
        {
            var result = a + dependency.Number;
            dependency.Number = result;
            return result;
        }

    }
}
