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

namespace Typemock.Examples.CSharp.StaticMethods
{
    /// <summary>
    /// This test class shows how to fake static methods. 
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)] // Note: Use Isolated to clean up after all tests in class
    public class StaticMethodsAndConstructors
    {
        [TestMethod]
        public void FakeAllStaticMethods()
        {
            Isolate.Fake.StaticMethods<Dependency>();

            var result = new ClassUnderTest().Calculate(1, 2);
            Assert.AreEqual(3, result);
        }   

        [TestMethod]
        public void FakeOneStaticMethod()
        {
            Isolate.WhenCalled(()=>Dependency.CheckSecurity(null,null)).IgnoreCall();
            
            var result = new ClassUnderTest().Calculate(1, 2);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void VerifyStaticMethodWasCalled()
        {
            Isolate.Fake.StaticMethods<Dependency>();

            var result = new ClassUnderTest().Calculate(1, 2);
            Isolate.Verify.WasCalledWithAnyArguments(() => Dependency.CheckSecurity(null, null));
        }

        /// <summary>
        /// This test shows to to fake calls to static constructors using Isolate.Fake.StaticConstructor().
        /// By default static constructors are called to fake them use Fake.StaticConstructor()
        /// </summary>
        [TestMethod]
        public void FakingStaticConstructor()
        {
            StaticConstructorExample.TrueOnStaticConstructor = false;
            Isolate.Fake.StaticConstructor<StaticConstructorExample>();

            // calling a static method on the class forces the static constructor to be called
            StaticConstructorExample.Foo();

            // this verifies the static constructor was faked and not called
            Assert.IsFalse(StaticConstructorExample.TrueOnStaticConstructor);
        }

        /// <summary>
        /// As static constructors for a type is only executed once, if we fake it we need a way to invoke it in a test that 
        /// requires normal execution.
        /// 
        /// Typemock Isolator does this automatically, but here is a way to force a static-constructor call
        /// </summary>
        [TestMethod]
        public void CallingStaticConstructorTest()
        {
            StaticConstructorExample.TrueOnStaticConstructor = false;

            // force static constructor to be called
            Isolate.Invoke.StaticConstructor(typeof(StaticConstructorExample));
            Assert.IsTrue(StaticConstructorExample.TrueOnStaticConstructor);
        }
    }



    //------------------
    // Classes under test
    // - Dependency: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    // - StaticConstructorExample: a class with a static constructor and a flag that indicates it was called.
    //------------------
 
    public class StaticConstructorExample
    {
        private static bool trueOnStaticConstructor = false;
        public static bool TrueOnStaticConstructor
        {
            get { return trueOnStaticConstructor; }
            set { trueOnStaticConstructor = value; }
        }

        static StaticConstructorExample()
        {
            trueOnStaticConstructor = true;
        }

        public static void Foo()
        {
        }
    }

    public class Dependency
    {
        public static void CheckSecurity(string name, string password)
        {
            throw new NotImplementedException();
        }
        public static void CallGuard()
        {
            throw new NotImplementedException();
        }
    }

    public class ClassUnderTest
    {
        public int Calculate(int a, int b)
        {
            Dependency.CheckSecurity("typemock", "rules");

            return a + b;
        }
    }
}
