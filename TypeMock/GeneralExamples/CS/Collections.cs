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

namespace Typemock.Examples.CSharp.Collections
{
    /// <summary>
    /// This test class shows ways to control methods and properties returning collections using Typemock Isolator.
    /// 
    /// Collection handling in done either explicitly by swapping collection values returned by a method with a collection
    /// of test data by using Isolate.WhenCalled().WillReturnCollectionValuesOf(), or by implicitly setting expectations on
    /// collection types.
    /// </summary>
    [TestClass, Isolated] // Note: Use Isolated to clean up after all tests in class
    public class Collections
    {
        [TestMethod]
        public void SwapCollection_WithFakeData()
        {
            var dependency = new Dependency();
            Isolate.WhenCalled(() => dependency.GetList())
                .WillReturnCollectionValuesOf(new int[] { 1, 2, 3 });
            
            var result = new ClassUnderTest().Sum(dependency);

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ImplictCollectionCreation_ByFakingLastItem()
        {
            var dependency = new Dependency();
            // A fake collection of size of 6 is created implicitely
            Isolate.WhenCalled(() => dependency.GetList()[5]).WillReturn(3);

            var result = new ClassUnderTest().Count(dependency);

            Assert.AreEqual(6, result);
            Assert.AreEqual(3, dependency.GetList()[5]);
        }   
    }

    //------------------
    // Classes under test
    // - MyList - a non implemented collection
    // - Dependency: Methods are not implemented - these need to be faked out, one returns a MyList
    // - ClassUnderTest: Class that uses Dependency
    //------------------

    public class MyList : IList<int>
    {
        #region IList implementation
        public int IndexOf(int item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public int this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(int item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(int item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(int item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class Dependency
    {
        public virtual MyList GetList()
        {
            throw new NotImplementedException();
        }
    }
    
    public class ClassUnderTest
    {
        public int Sum(Dependency dependency)
        {
            int total = 0;
            foreach (var i in dependency.GetList())
            {
                total += i;
            }
            return total;
        }

        public int Count(Dependency dependency)
        {
            return dependency.GetList().Count;
        }

    }
}
