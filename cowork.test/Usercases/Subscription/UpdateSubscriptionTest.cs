using System;
using cowork.domain.Interfaces;
using cowork.usecases.Subscription;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace cowork.test.Usercases.Subscription {

    [TestFixture]
    public class UpdateSubscriptionTest {

        [Test]
        public void ShouldUpdateSubscription() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true);
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.Update(sub)).Returns(0);

            var res = new UpdateSubscription(mockSubRepo.Object, sub).Execute();
            Assert.AreEqual(0, res);
        }
        
        
        [Test]
        public void ShouldFailUpdatingSubscription() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true);
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.Update(It.IsAny<domain.Subscription>())).Returns(-1);

            var res = new UpdateSubscription(mockSubRepo.Object, sub).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}