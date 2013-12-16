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

namespace Typemock.Examples.CSharp.FakingConstructors
{
    /// <summary>
    /// This test class demonstrates controlling arguments passed to constructor of a fake 
    /// and controlling the constructors that are called
    /// </summary>
    [TestClass, Isolated(DesignMode.Pragmatic)] // Note: Use Isolated to clean up after all tests in class
    public class FakingConstructors
    {
        [TestMethod ]
        public void CallConstructorAndPassArguments_FakeAllMethods()
        {
            // The constructor is not faked here.      
            var fake = Isolate.Fake.Instance<Dependency>
                (Members.ReturnRecursiveFakes, ConstructorWillBe.Called, 5, "Typemock");
             
            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.GetString(fake);

            Assert.AreEqual("Typemock5", result);
        }
          

        [TestMethod]
        public void IgnoringOnlyConstrutor_RestOfMethodsCalled()
        {
            var fake = Isolate.Fake.Instance<Dependency>
                (Members.CallOriginal, ConstructorWillBe.Ignored);

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.GetString(fake);

            Assert.AreEqual("0", result);
 
        }

        [TestMethod]
        public void FutureInstance_VerifyThrowingExceptionOnCreation()
        {
            // We want a memory handling exception to be thrown the next time a Dependency is instantiated
            Isolate.Swap.NextInstance<Dependency>()
                .ConstructorWillThrow(new OutOfMemoryException());

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.Create();

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void CallConstructor_FakeBaseClassConstructor()
        {
            // create an instance of Derived, but avoid calling the base class constructor
            var dependency = Isolate.Fake.Instance<Derived>(Members.CallOriginal, ConstructorWillBe.Called,
                                                            BaseConstructorWillBe.Ignored);

            var classUnderTest = new ClassUnderTest();
            var result = classUnderTest.GetSize(dependency);

            Assert.AreEqual(100, result);
        }
    }

    //------------------
    // Classes under test
    // - Dependency: Class with Methods that need to be faked out
    // - ClassUnderTest: Class that creates and uses Dependency
    // - Base and Derived: Class Hierarchy but Base still needs to implement its constructor
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

        public virtual void DoSomething ()
        {
            throw new NotImplementedException();
        }
    }

    public class Base
    {
        public Base()
        {
            throw new NotImplementedException(); 
        }

        public virtual int Size { get; set; }
    }

    public class Derived : Base
    {
        public Derived()
        {
            Size = 100;
        }
    }

    public class ClassUnderTest
    {
        public string GetString(Dependency user)
        {
            return user.Name + user.Age.ToString();

        }

        public Dependency Create()
        {
            try
            {
                return new Dependency(0, "");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetSize(Derived derived)
        {
            return derived.Size;
        }
    }
}
