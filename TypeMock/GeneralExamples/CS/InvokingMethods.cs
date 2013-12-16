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

namespace Typemock.Examples.CSharp.InvokingMethods
{
    /// <summary>
    /// This class demonstrates the ability of firing events and invoking private methods using Isolator.
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)]  // Note: Use Isolated to clean up after all tests in class
    public class InvokingMethods
    {
        [TestMethod]
        public void FireEvent_RunEvent()
        {
            var underTest = new ClassUnderTest();
            var counter = new Counter(underTest);

            // Note how adding a dummy event is the way to fire it
            Isolate.Invoke.Event(() => underTest.RunEvent+= null, 0);

            Assert.AreEqual(1, counter.Times);
        }

        [TestMethod]
        public void InvokePrivateMethod()
        {
            var underTest = new ClassUnderTest();

            var result = Isolate.Invoke.Method(underTest, "Sum", 2, 5);

            Assert.AreEqual(7, result);
        }
 
        [TestMethod]
        public void InvokePrivateStaticMethod()
        {
            var result = Isolate.Invoke.Method<ClassUnderTest>("Multiply", 2, 5);

            Assert.AreEqual(10, result);
        }
    }

    //------------------
    // Classes under test
    // - Dependency: Class with Methods that need to be faked out
    // - ClassUnderTest: Class that creates and uses Dependency
    // - Counter: A Class that registers to our ClassUnderTest events
    //------------------

    public class Dependency
    {
        public int Age;
        public string Name;
        public Dependency(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }


    public class Counter
    {
        public int Times { get; set; }
        public Counter(ClassUnderTest underTest)
        {
            underTest.RunEvent += new Action<int>(underTest_RunEvent);
        }

        void underTest_RunEvent(int obj)
        {
            Times++;
        }
    }


    public class ClassUnderTest
    {
        public event Action<int> RunEvent;

        public ClassUnderTest() { }

        private int Sum(int a, int b)
        {
            return a + b;
        }

        private static int Multiply(int a, int b)
        {
            return a * b;
        }
    }
}
