using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Place;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.PlaceTests {

    [TestFixture]
    public class GetPlaceByIdTest {

        [Test]
        public void ShouldGetPlaceById() {
            var place = new Place(0,"test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.GetById(0)).Returns(place);

            var time = new TimeSlot(DayOfWeek.Monday, 8, 30, 18, 30, 0);
            
            var mockTimeSlotRepo = new Mock<ITimeSlotRepository>();
            mockTimeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> { time });
            var res = new GetPlaceById(mockPlaceRepo.Object, mockTimeSlotRepo.Object, 0).Execute();
            Assert.NotNull(res);
            Assert.AreEqual("test",res.Name);
            Assert.AreEqual(time, res.OpenedTimes.First());
        }
        
        
        [Test]
        public void ShouldFailGettingPlaceById() {
            var place = new Place(0,"test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.GetById(1)).Returns(place);

            var time = new TimeSlot(DayOfWeek.Monday, 8, 30, 18, 30, 0);
            
            var mockTimeSlotRepo = new Mock<ITimeSlotRepository>();
            mockTimeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> { time });
            var res = new GetPlaceById(mockPlaceRepo.Object, mockTimeSlotRepo.Object, 0).Execute();
            Assert.IsNull(res);
        }

    }

}