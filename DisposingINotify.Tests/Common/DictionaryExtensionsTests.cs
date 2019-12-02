using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;

namespace DisposingINotify.Tests.Common
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void TryAddTest1()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val = new object();
            var key = "Key1";
            target.TryAdd(key, val);
            Assert.AreEqual(1, target.Count);
            var innerDict = target[key];
            Assert.AreEqual(1, innerDict.Count);
        }
        [TestMethod]
        public void TryAddTest2()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val1 = new object();
            var val2 = new object();
            var key = "Key1";
            target.TryAdd(key, val1);
            target.TryAdd(key, val2);
            Assert.AreEqual(1, target.Count);
            var innerDict = target[key];
            Assert.AreEqual(2, innerDict.Count);
        }

        [TestMethod]
        public void TryAddTest3()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val1 = new object();
            var key1 = "Key1";
            var key2 = "Key2";
            target.TryAdd(key1, val1);
            target.TryAdd(key2, val1);
            Assert.AreEqual(2, target.Count);
            var innerDict = target[key1];
            Assert.AreEqual(1, innerDict.Count);
        }

        [TestMethod]
        public void TryRemoveTest1()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val = new object();
            var key = "Key1";
            target.TryAdd(key, val);
            Assert.AreEqual(1, target.Count);
            var innerDict = target[key];
            Assert.AreEqual(1, innerDict.Count);
            target.TryRemove(key, val);
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void TryRemoveTest2()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val1 = new object();
            var val2 = new object();
            var key = "Key1";
            target.TryAdd(key, val1);
            target.TryAdd(key, val2);
            Assert.AreEqual(1, target.Count);
            var innerDict = target[key];
            Assert.AreEqual(2, innerDict.Count);
            target.TryRemove(key, val2);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(1, innerDict.Count);
            target.TryRemove(key, val1);
            Assert.AreEqual(0, target.Count);
            Assert.AreEqual(0, innerDict.Count);
        }

        [TestMethod]
        public void TryRemoveTest3()
        {
            var target = new Dictionary<string, Dictionary<object, string>>();
            var val1 = new object();
            var key1 = "Key1";
            var key2 = "Key2";
            target.TryAdd(key1, val1);
            target.TryAdd(key2, val1);
            Assert.AreEqual(2, target.Count);
            var innerDict = target[key1];
            Assert.AreEqual(1, innerDict.Count);
            target.TryRemove(key1, val1);
            Assert.AreEqual(1, target.Count);
            target.TryRemove(key2, val1);
            Assert.AreEqual(0, target.Count);
        }
    }
}
