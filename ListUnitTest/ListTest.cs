using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollections;
using System.Collections.Generic;
using System.Collections;

namespace ListUnitTest
{
    [TestClass]
    public class ListUnitTest
    {
        MyList<int> list;

        [TestInitialize]
        public void InitializeCurrentTest()
        {
            list = new MyList<int>();
        }

        [TestMethod]
        public void CtorTestMethod()
        {
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(false, list.IsReadOnly);
        }

        [TestMethod]
        public void CtorWithArgument()
        {
            int[] arr = new[] { 2, 5, 7, 9 };
            list = new MyList<int>(arr);

            CollectionAssert.AreEqual(arr, list);
            Assert.AreNotEqual(null, list);
        }
        [TestMethod]
        public void AddTestMethod()
        {
            Assert.AreNotEqual(null, list);
            Assert.AreEqual(0, list.Count);
            list.Add(10);
            Assert.AreEqual(1, list.Count);

            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(101, list.Count);
        }
        [TestMethod]
        public void ClearTestMethod()
        {
            for (int i = 0; i < 100; ++i)
            {
                list.Add(i);
            }
            Assert.AreEqual(100, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void IndexTestMethod()
        {
            int[] arr = { 1, 5, 10, 15, 25, 3, 8, 8 };
            foreach (int val in arr)
            {
                list.Add(val);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                Assert.AreEqual(arr[i], list[i]);
            }
        }

        [TestMethod]
        public void Index_12_expected12()
        {
            int s = 12;
            int expected = 12;

            list[0] = s;
            int actual = list[0];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTestMethod_1()
        {
            list[-1] = 100;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTestMethod_2()
        {
            list[1] = 100;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTestMethod_3()
        {
            int x = list[-1];
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTestMethod_4()
        {
            int x = list[1];
        }
        
        [TestMethod]
        public void CopyToCollectionTest()
        {
            list.Add(54);
            list.Add(78);
            list.Add(19);
            list.Add(35);
            list.Add(46);
            list.Add(57);
            int[] arr = new int[list.Count];

            list.CopyTo(arr, 0);

            CollectionAssert.AreEqual(list, arr);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            list.Add(5);
            list.Add(88);
            list.Add(2018);
            int expected = 2;

            int actual = list.IndexOf(2018);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveTest()
        {
            list.Add(19);
            list.Add(42);
            list.Add(25);
            bool expected = false;

            list.Remove(42);
            bool actual = list.Contains(42);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Insert_indexAndValueTest()
        {
            list.Add(8);
            int expected = 12;
            list.Insert(1, 12);
            int actual = list[1];

            Assert.AreEqual(2, list.Count, "This wrong count");
            Assert.AreEqual(expected, actual, "This wrong inset");
        }

        [TestMethod()]
        public void EnumerableTest()
        {
            int[] inArr = new[] { 23, 45, 67, 86, 9 };
            list = new MyList<int>(inArr);
            int[] checkArr = new int[inArr.Length];
            int index = 0;

            foreach (var item in list)
            {
                checkArr[index++] = item;
            }
            CollectionAssert.AreEqual(inArr, checkArr);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            list.Add(11);
            list.Add(21);
            list.Add(31);
            bool expected = false;
            int expectedCount = 2;

            list.RemoveAt(1);
            bool actual = list.Contains(21);
            int actualCount = list.Count;
            Assert.AreEqual(expectedCount, actualCount, "This not right count");
            Assert.AreEqual(expected, actual, "This did not remove");
        }
    }
}