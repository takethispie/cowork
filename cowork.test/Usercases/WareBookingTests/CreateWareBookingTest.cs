using System;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.WareBooking;
using cowork.usecases.WareBooking.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.WareBookingTests {

    [TestFixture]
    public class CreateWareBookingTest {

        [Test]
        public void ShouldCreateWareBooking() {
            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 0, 0, 0);
            var input = new CreateWareBookingInput(0, 0, now, now.Date.AddHours(1));
            var domain = new WareBooking( 0, 0, now, now.AddHours(1));
            
            var mockWareBookingRepo = new Mock<IWareBookingRepository>();
            mockWareBookingRepo.Setup(m => m.GetStartingAt(now.Date)).Returns(new List<WareBooking>());
            mockWareBookingRepo.Setup(m => m.Create(domain)).Returns(0);

            var ware = new Ware("test", "test", "e1234fjdgq84324", 0, false);
            var mockWareRepo = new Mock<IWareRepository>();
            mockWareRepo.Setup(m => m.GetById(0)).Returns(ware);
            
            var mockTimeSlotRepo = new Mock<ITimeSlotRepository>();
            mockTimeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> {
                new TimeSlot(now.DayOfWeek, 8, 0, 20, 0, 0)
            });

            var res = new CreateWareBooking(mockWareBookingRepo.Object, mockTimeSlotRepo.Object, mockWareRepo.Object,
                input).Execute();
            Assert.AreEqual(0, res);
        }

    }

}