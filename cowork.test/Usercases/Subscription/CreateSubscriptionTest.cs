using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Subscription;
using cowork.usecases.Subscription.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Subscription {

    [TestFixture]
    public class CreateSubscriptionTest {

        [Test]
        public void ShouldCreateSubscription() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true);
            var input = new CreateSubscriptionInput(0, 0, 0, now, true, new domain.SubscriptionType());
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.Create(sub)).Returns(0);

            var res = new CreateSubscription(mockSubRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }
        
        [Test]
        public void ShouldFailCreatingSubscription() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true);
            var input = new CreateSubscriptionInput(0, 0, 0, now, true, new domain.SubscriptionType());
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.Create(It.IsAny<domain.Subscription>())).Returns(-1);

            var res = new CreateSubscription(mockSubRepo.Object, input).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}