using System;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Subscription;
using cowork.usecases.Subscription.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Subscription {

    [TestFixture]
    public class GetSubscriptionByIdTest {

        [Test]
        public void ShouldGetSubscriptionById() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true) {Place = new domain.Place()};
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.GetById(0)).Returns(sub);
            
            var time = new TimeSlot(DayOfWeek.Monday, 8, 30, 18, 30, 0);
            var mockTimeSlotRepo = new Mock<ITimeSlotRepository>();
            mockTimeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> { time });
            
            var res = new GetSubscriptionById(mockSubRepo.Object, mockTimeSlotRepo.Object, 0).Execute();
            Assert.NotNull(res);
            Assert.AreEqual(sub.LatestRenewal, res.LatestRenewal);
            Assert.AreEqual(true, res.FixedContract);
        }
        
        
        [Test]
        public void ShouldFailGettingSubscriptionById() {
            var now = DateTime.Now;
            var sub = new domain.Subscription(0, 0, 0, now, 0, true) {Place = new domain.Place()};
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.GetById(0)).Returns(sub);
            
            var time = new TimeSlot(DayOfWeek.Monday, 8, 30, 18, 30, 0);
            var mockTimeSlotRepo = new Mock<ITimeSlotRepository>();
            mockTimeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> { time });
            
            var res = new GetSubscriptionById(mockSubRepo.Object, mockTimeSlotRepo.Object, 1).Execute();
            Assert.IsNull(res);
        }

    }

}