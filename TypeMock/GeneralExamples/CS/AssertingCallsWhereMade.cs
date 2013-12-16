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

namespace Typemock.Examples.CSharp.AssertingCallsWhereMade
{
    /// <summary>
    /// This test class shows different ways of verifying calls on fake objects using the Isolate.Verify API.
    /// Calls can be verified in the following ways:
    ///     - WasCalledWithAnyArguments(): verifies the call was made regardless of arguments passed to it
    ///     - WasCalledWithExactArguments(): verifies the call was made with the exact arguments specified
    ///     - WasNotCalled: verifies the call was not made
    ///     - WasCalledWithArguments: use Matching to match arguments
    /// </summary>
    [TestClass, Isolated] // Note: Use Isolated to clean up after all tests in class
    public class VerifyingCallsWhereMade
    {
         
        [TestMethod]
        public void Verify_CallWasMade_WithAnyArgument()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();
           
            new ClassUnderTest().DoAction(2, fakeDependency);

            Isolate.Verify.WasCalledWithAnyArguments(() => fakeDependency.CheckSecurity(null, null));
        }

        [TestMethod]
        public void Verify_CallWasNeverMade()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();

            new ClassUnderTest().DoAction(2, fakeDependency);

            Isolate.Verify.WasNotCalled(() => fakeDependency.CallGuard());
        }

        [TestMethod]
        public void Verify_CallWasMadeTwice()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();

            var cut = new ClassUnderTest();
            cut.DoAction(2, fakeDependency);
            cut.DoAction(3, fakeDependency);

            int count = Isolate.Verify.GetTimesCalled(() => fakeDependency.CheckSecurity("",""));
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void Verify_CallWasNeverMade_OnChain()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();

            new ClassUnderTest().DoAction(2, fakeDependency);

            Isolate.Verify.WasNotCalled(() => fakeDependency.CallGuard().CheckSecurity(null,null));
        }

        [TestMethod]
        public void Verify_CallWasMade_WithExactArguments()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();

            new ClassUnderTest().DoAction(2, fakeDependency);

            Isolate.Verify.WasCalledWithExactArguments(() => fakeDependency.CheckSecurity("typemock", "rules"));
        }

        [TestMethod]
        public void Verify_CallWasMade_WithMatchingArguments()
        {
            var fakeDependency = Isolate.Fake.Instance<Dependency>();

            new ClassUnderTest().DoAction(2, fakeDependency);

            Isolate.Verify.WasCalledWithArguments(() => fakeDependency.CheckSecurity(null, null)).Matching(
                a => (a[0] as string).StartsWith("type") &&
                     (a[1] as string).StartsWith("rule"));
        }
    }

    //------------------
    // Classes under test
    // - Dependency: Methods are not implemented - these need to be faked out
    // - ClassUnderTest: Class that uses Dependency
    //------------------

    public class Dependency
    {
        public virtual void CheckSecurity(string name, string password)
        {
            throw new NotImplementedException();
        }
        public virtual Dependency CallGuard()
        {
            throw new NotImplementedException();
        }
    }

    public class ClassUnderTest
    {

        public void DoAction(int i, Dependency fakeDependency)
        {
            fakeDependency.CheckSecurity("typemock","rules");
   
            if (i<2)
                fakeDependency.CallGuard();
        }

    }
}
