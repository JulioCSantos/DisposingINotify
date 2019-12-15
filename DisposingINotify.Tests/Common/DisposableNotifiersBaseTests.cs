using System;
using System.Linq;
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
            Assert.IsFalse(subj1.Pub1PropertyChanged);
            target.OneProperty = string.Empty;
            Assert.IsTrue(subj1.Pub1PropertyChanged);
        }

        [TestMethod]
        public void UnsubscribedTest()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            Assert.IsFalse(subj1.Pub1PropertyChanged);
            target.OneProperty = "Joe1";
            Assert.IsTrue(subj1.Pub1PropertyChanged);

            subj1.UnSubscribePublisher1();
            subj1.Pub1PropertyChanged = false;
            target.OneProperty = "Joe2";
            Assert.IsFalse(subj1.Pub1PropertyChanged);

        }
    }
}
