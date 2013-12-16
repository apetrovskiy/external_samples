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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Typemock.Examples.CSharp.FakingDependencies
{
    /// <summary>
    /// This test class shows different ways of faking dependencies of a class.
    /// This method, creates a type by creating fakes for the longest constructor and calling it.
    /// This is a great tool to make sure that adding dependencies to the type won't fail the tests.
    /// </summary>
    [TestClass, Isolated] // Note: Use Isolated to clean up after the test
    public class FakingDependencies
    {
        [TestMethod]
        public void FakeAllDependenciesInTheConstructor()
        {
            var realClassUnderTest = Isolate.Fake.Dependencies<ClassUnderTest>();
            var result = realClassUnderTest.Calculate(1, 2);
            Assert.AreEqual(0,result);
        }

        [TestMethod]
        public void FakeAllDependencies_ChangeBehaviorOfADependency()
        {
            var realClassUnderTest = Isolate.Fake.Dependencies<ClassUnderTest>();
            var fakeDependency = Isolate.GetFake<Dependency>(realClassUnderTest);
            Isolate.WhenCalled(() => fakeDependency.Multiplier).WillReturn(2);

            var result = realClassUnderTest.Calculate(1, 2);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void FakeAllDependencies_OverrideDefaultArguments()
        {
            var realDependency = new Dependency();
            var realClassUnderTest = Isolate.Fake.Dependencies<ClassUnderTest>(realDependency,4);

            var result = realClassUnderTest.Calculate(1, 2);
            Assert.AreEqual(7, result);
        }
   }

    //------------------
    // Classes under test
    // - Dependency: A dependency of ClassUnderTest
    // - Dependency2: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------


    public class Dependency
    {
        public virtual int  Multiplier { get; set; }
        public Dependency()
        {
            Multiplier = 1;
        }
    }

    public class Dependency2
    {
        public virtual int Check()
        {
            throw new NotImplementedException();
        }
    }
    public class ClassUnderTest
    {
        private int additional;
        private Dependency2 d2;
        private Dependency d1;

        public ClassUnderTest(int additional, Dependency2 d2, Dependency d1)
        {
            this.additional = additional;
            this.d2 = d2;
            this.d1 = d1;
        }

        public int Calculate(int a, int b)
        {
            d2.Check();
            return (a + b)*d1.Multiplier+additional;
        }
    }
}
