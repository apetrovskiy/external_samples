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

namespace Typemock.Examples.CSharp.Future
{
    /// <summary>
    /// This test class demonstrates handling of objects instantiated outside the test's scope. 
    /// This is useful to eliminate dependencies in objects created by the business logic being tested
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)]// Note: Use Isolated to clean up after all tests in class
    public class CreatingFutureFakes
    {
        [TestMethod]
        public void Fake_SingleFutureInstance()
        {
            var fake = Isolate.Fake.NextInstance<Dependency>();

            var result = ClassUnderTest.AddCheckingInternalDependency(1, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Fake_MultipleFutureInstances()
        {
            var fake = Isolate.Fake.AllInstances<Dependency>();
            
            var result = ClassUnderTest.AddCheckingTwoInternalDependencies(1, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void FakeSingleton()
        {
            // Here we are setting the same behavior on all instances.
            // The behavior we set on fake will apply to past instance as well
            var fakeSingleton = Isolate.Fake.AllInstances<Singleton>();
            Isolate.WhenCalled(() => fakeSingleton.ReturnZero()).WillReturn(10);

            // Assert that the behavior works.
            Assert.AreEqual(10, Singleton.Instance.ReturnZero());
        }

        [TestMethod]
        public void Fake_ImplementedDependency()
        {
            var fake = Isolate.Fake.NextInstance<IDependency>();
            
            var result = ClassUnderTest.AddCheckingDerivedDependency(1, 2);

            Assert.AreEqual(3, result);
        }

    }


    //------------------
    // Classes under test
    // - Dependency: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    // - Singleton: A Singleton 
    //------------------
    public class Dependency
    {
        public void Check(int x, int y)
        {
            throw new Exception("Not checked!");
        }

    }


    public interface IDependency
    {
        void Check(int x, int y);
    }
    
    public class ConcreteDependency : IDependency
    {
        public void Check(int x, int y)
        {
            throw new Exception("Not checked!");
        }
    }
        

    public class ClassUnderTest
    {
        public static int AddCheckingInternalDependency(int x, int y)
        {
            var dependency = new Dependency();
            dependency.Check(x,y);
            
            return x + y;
        }

        public static int AddCheckingDerivedDependency(int x, int y)
        {
            var dependency = new ConcreteDependency();
            dependency.Check(x,y);
            
            return x + y;
        }

        public static int AddCheckingTwoInternalDependencies(int x, int y)
        {
            var dependency = new Dependency();
            dependency.Check(x,y);

            var dependency2 = new Dependency();
            dependency2.Check(x,y);

            return x + y;
        }
    }

    public class Singleton
    {
        private Singleton() { }
        static readonly Singleton instance = new Singleton();

        public static Singleton Instance { get {  return instance; } }

        public int ReturnZero()
        {
            return 10;
        }
    }
}
