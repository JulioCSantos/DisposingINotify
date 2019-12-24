using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
        public void UnsubscribedTest3()
        {
            var subj1Instance1 = new SubscriberSubject1();
            var subj1Instance2 = new SubscriberSubject1();
            var target = subj1Instance1.Publisher1 = subj1Instance2.Publisher1 = new PublisherSubject1();
            subj1Instance1.SubscribePublisher1();
            subj1Instance2.SubscribePublisher1();
            Assert.AreEqual(0, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(0, subj1Instance2.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(1, subj1Instance2.Pub1PropertyChanges);
            subj1Instance1.UnSubscribePublisher1();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(2, subj1Instance2.Pub1PropertyChanges);
        }

        [TestMethod]
        public void UnsubscribedTest4()
        {
            var subj1Instance1 = new SubscriberSubject1();
            var subj1Instance2 = new SubscriberSubject1();
            var target = subj1Instance1.Publisher1 = subj1Instance2.Publisher1 = new PublisherSubject1();
            subj1Instance1.SubscribePublisher1();
            subj1Instance2.SubscribePublisher1();
            Assert.AreEqual(0, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(0, subj1Instance2.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(1, subj1Instance2.Pub1PropertyChanges);
            target.Dispose();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj1Instance1.Pub1PropertyChanges);
            Assert.AreEqual(1, subj1Instance2.Pub1PropertyChanges);
        }

        [TestMethod]
        public void SubscriptionCountsTest2()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(1, target.GetMethodsNamesList().Count());
            target.Dispose();
            Assert.AreEqual(0, target.GetMethodsNamesList().Count());
        }

        [TestMethod]
        public void SubscriptionDisposingEventTest1()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(1, target.GetMethodsNamesList().Count());
            var cancelHandlerInvoked = false;
            CancelEventHandler handler = (s, e) => { cancelHandlerInvoked = true; };
            subj1.Publisher1.DisposingHandlers += handler;
            target.Dispose();
            Assert.IsTrue(cancelHandlerInvoked);
            Assert.AreEqual(0, target.GetMethodsNamesList().Count());
        }

        [TestMethod]
        public void SubscriptionDisposingEventTest2()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.AreEqual(1, target.GetMethodsNamesList().Count());
            var cancelHandlerInvoked = false;
            CancelEventHandler handler = (s, e) => { cancelHandlerInvoked = true; e.Cancel = true; };
            subj1.Publisher1.DisposingHandlers += handler;
            target.Dispose();
            Assert.IsTrue(cancelHandlerInvoked);
            Assert.AreEqual(1, target.GetMethodsNamesList().Count());
        }

        [TestMethod]
        public void GetListsTest1()
        {
            var subj1 = new SubscriberSubject1();
            var subj2 = new SubscriberSubject2();
            var target = subj1.Publisher1 = subj2.Publisher1 = new PublisherSubject1();
            subj1.SubscribePublisher1();
            subj2.SubscribePublisher1();
            Assert.AreEqual(2, target.GetInvocationList().Count());
            Assert.AreEqual(2, target.GetSubscribersTypesList().Count());
            Assert.AreEqual(1, target.GetMethodsNamesList().Count());
            target.Dispose();
            Assert.AreEqual(0, target.GetInvocationList().Count());
            Assert.AreEqual(0, target.GetSubscribersTypesList().Count());
            Assert.AreEqual(0, target.GetMethodsNamesList().Count());
        }

        [TestMethod]
        public void UnsubscribedAllTest2()
        {
            var subj1 = new SubscriberSubject1();
            var subj2 = new SubscriberSubject2();
            subj1.Publisher1 = new PublisherSubject1();
            subj2.Publisher1 = subj1.Publisher1;
            var target = subj1.Publisher1;

            Assert.IsTrue(target == subj1.Publisher1);
            Assert.IsTrue(subj1.Publisher1 == subj2.Publisher1);
            Assert.IsTrue(target == subj2.Publisher1);
            subj1.SubscribePublisher1();
            subj2.SubscribePublisher1();
            Assert.AreEqual(0, subj1.Pub1PropertyChanges);
            Assert.AreEqual(0, subj2.Pub1PropertyChanges);
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            target.Dispose();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj1.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
        }

        [TestMethod]
        public void MultipleSubscriptionsTest1()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();
            Assert.AreEqual(0, target.GetInvocationList().Count());
            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            Assert.AreEqual(2, target.GetInvocationList().Count());
            Assert.AreEqual(1, target.GetSubscribersTypesList().Count());
            Assert.AreEqual(2, target.GetMethodsNamesList().Count());

            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub2PropertyChanges);
            subj2.UnSubscribePublisher1();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            Assert.AreEqual(2, subj2.Pub2PropertyChanges);
        }

        [TestMethod]
        public void MultipleSubscriptionsTest2()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();

            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub2PropertyChanges);
            subj2.UnSubscribePublisher2();
            target.OneProperty = "Joe2";
            Assert.AreEqual(2, subj2.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub2PropertyChanges);
        }

        [TestMethod]
        public void MultipleSubscriptionsTest3()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();

            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            target.OneProperty = "Joe1";
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub2PropertyChanges);
            target.Dispose();
            target.OneProperty = "Joe2";
            Assert.AreEqual(1, subj2.Pub1PropertyChanges);
            Assert.AreEqual(1, subj2.Pub2PropertyChanges);
        }

        [TestMethod]
        public void NullSetTest1()
        {
            var subj1 = new SubscriberSubject1();
            subj1.Publisher1 = new PublisherSubject1();
            var target = subj1.Publisher1;
            Assert.AreEqual(target.GetHashCode(), subj1.Publisher1.GetHashCode());
            subj1.SubscribePublisher1();
            Assert.AreEqual(1, target.GetInvocationList().Count());
            subj1.Publisher1.Dispose();
            subj1.Publisher1 = null;
            Assert.AreEqual(0, target.GetInvocationList().Count());
        }

        [TestMethod]
        public void NullSetTest2()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();
            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            Assert.AreEqual(2, target.GetInvocationList().Count());
            subj2.Publisher1.DisposedHandlers += (s, e) => { subj2.Publisher2 = null; };
            subj2.Publisher1.Dispose();
            subj2.Publisher1 = null;
            Assert.AreEqual(0, target.GetInvocationList().Count());
        }

        [TestMethod]
        public void DisposeMultipleTimesTest1()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();
            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            Assert.AreEqual(2, target.GetInvocationList().Count());
            subj2.Publisher1.DisposedHandlers += (s, e) => { subj2.Publisher2 = null; };
            subj2.Publisher1.Dispose();
            subj2.Publisher1.Dispose();
            subj2.Publisher1.Dispose();
            subj2.Publisher1 = null;
            Assert.AreEqual(0, target.GetInvocationList().Count());
        }

        [TestMethod]
        public void SetToNullAfterDisposedTest1()
        {
            var subj2 = new SubscriberSubject2();
            var target = subj2.Publisher1 = subj2.Publisher2 = new PublisherSubject1();
            subj2.SubscribePublisher1();
            subj2.SubscribePublisher2();
            Assert.AreEqual(2, target.GetInvocationList().Count());
            subj2.Publisher1.DisposedHandlers += (s, e) => { subj2.Publisher1 = null; subj2.Publisher2 = null; };
            subj2.Publisher1.Dispose();
            Assert.AreEqual(0, target.GetInvocationList().Count());
            Assert.IsNull(subj2.Publisher1);
            Assert.IsNull(subj2.Publisher2);
        }
    }
}
