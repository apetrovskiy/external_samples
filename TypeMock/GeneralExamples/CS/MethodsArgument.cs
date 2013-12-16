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

namespace Typemock.Examples.CSharp.MethodsArgumentExamples
{
    /// <summary>
    /// This test class shows different ways of controlling the behavior of fake objects using the Isolate.WhenCalled() API.
    /// The supported behaviors are:
    /// <list type="bullet">
    ///     <item>Using Exact Argument Matching</item>
    ///     <item>Using Custom Checkers on Arguments</item>
    ///     <item>Mixing WithExactArguments and Custom Checkers</item>
    /// </list>
    /// </summary>
    [TestClass, Isolated] // Note: Use Isolated to clean up after all tests in class
    public class MethodsArgument
    {
        [TestMethod]
        public void FakeReturnValue_BasedOn_ExactMethodArgumentsAtRuntime()
        {
            var fake = Isolate.Fake.Instance<Dependency>();
            Isolate.WhenCalled(() => fake.MethodReturnInt("typemock",1)).WithExactArguments().WillReturn(10);
            Isolate.WhenCalled(() => fake.MethodReturnInt("unit tests", 2)).WithExactArguments().WillReturn(50);

            var classUnderTest =  new ClassUnderTest();
            var result = classUnderTest.SimpleCalculation(fake);
            Assert.AreEqual(60,result);
        }


        [TestMethod]
        public void FakeVoidMethod_BasedOn_ExactMethodArgs()
        {
            var realDependency = new Dependency();
            Isolate.WhenCalled(() => realDependency.VoidMethod(4))
                .WithExactArguments().IgnoreCall();

            bool exceptionWasThrown = false;
            try
            {
                var classUnderTest = new ClassUnderTest();
                classUnderTest.CallVoid(realDependency, 4);
            }
            catch (NotImplementedException)
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);
        }

        [TestMethod]
        public void FakeReturnValue_BasedOnCustomArgumentsChecking()
        {
            var fake = Isolate.Fake.Instance<Dependency>();

            Isolate.WhenCalled((string s, int x) => fake.MethodReturnInt(s, x))
                .AndArgumentsMatch((s, x) => s.StartsWith("Gui") && x < 300)
                .WillReturn(1000);

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.CallWithGuitar100(fake);
            
            // All the arguments match our custom checker - the returned value is faked.
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void FakeReturnValue_BasedOnCustomArgumentsChecking_CheckOneArgument()
        {
            var fake = Isolate.Fake.Instance<Dependency>();

            Isolate.WhenCalled((int x) => fake.MethodReturnInt("", x))
            .AndArgumentsMatch( x => x < 300)
            .WillReturn(1000);

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.CallWithGuitar100(fake);
            // All the arguments match our custom checker - the returned value is faked.
            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void FakeReturnValue_BasedOn_MixedChecker()
        {
            var fake = Isolate.Fake.Instance<Dependency>();

            Isolate.WhenCalled((int x) => fake.MethodReturnInt("Guitar", x))
                .AndArgumentsMatch(x => x < 300)
                .WithExactArguments()
                .WillReturn(1000);

            var result = new ClassUnderTest().CallWithGuitar100(fake);
            // All the arguments match our custom checker - the returned value is faked.
            Assert.AreEqual(1000, result);
        }
    }

    //------------------
    // Classes under test
    // - Dependency: Class with Methods that need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------

    public class Dependency
    {
        public virtual int MethodReturnInt(string arg1, int arg2)
        {
            throw new NotImplementedException();
        }
        public virtual void VoidMethod(int n)
        {
            throw new NotImplementedException();
        }
    }

    public class ClassUnderTest
    {
        public int SimpleCalculation( Dependency dependency)
        {
            return dependency.MethodReturnInt("typemock", 1) + dependency.MethodReturnInt("unit tests", 2);
        }

        public void CallVoid( Dependency dependency,int i)
        {
            dependency.VoidMethod(i);
        }

        public int CallWithGuitar100(Dependency dependency)
        {
            return dependency.MethodReturnInt("Guitar", 200);
        }
    }
}
