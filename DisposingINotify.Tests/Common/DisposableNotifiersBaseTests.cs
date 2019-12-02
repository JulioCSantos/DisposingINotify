using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestExtensions;

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
        public void NotificationTests()
        {
            var subj1 = new SubscriberSubject1();
            var target = subj1.Publisher1;
            subj1.SubscribePublisher1();
            subj1.Publisher1
                .ShouldNotifyOn((p) => p.OneProperty )
                .When( a => target.OneProperty = string.Empty);

        }
    }
}
