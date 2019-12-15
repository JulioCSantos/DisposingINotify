using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DisposingINotify.Tests.Common.TestingSubjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DisposingINotify.Tests.Common
{
    [TestClass]
    public class DisposableNotifiersBaseTests
    {
        [TestMethod]
        public void InstantiateADisposableObservableObject()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            Assert.IsNotNull(subj1);
            Assert.IsFalse(target.GetInvocationList().Any());
            subj1.SubscribePublisher1();
            Assert.IsTrue(target.GetInvocationList().Any());
            Assert.AreEqual(1,target.GetInvocationList().Count());
        }

        [TestMethod]
        public void NotificationTest()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(0, subj1.Pub1PropertyChanges);
            target.OneProperty = string.Empty;
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
        }

        [TestMethod]
        public void SubscriptionTest1()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(0, subj1.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
            target.OneProperty = "Joe2";
            Assert.AreEqual(2, subj1.Pub1PropertyChanges);
        }

        [TestMethod]
        public void UnsubscribedTest1()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(0,subj1.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
            subj1.UnSubscribePublisher1();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
        }

        [TestMethod]
        public void UnsubscribedTest2()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(0, subj1.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
            target.Dispose();
            //target = new PublisherSubject1();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
        }
        [TestMethod]
        public void SubscriptionCountsTest2()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(1, target.GetSubscribersList().Count());
            target.Dispose();
            Assert.AreEqual(0, target.GetSubscribersList().Count());
        }
    }
}
