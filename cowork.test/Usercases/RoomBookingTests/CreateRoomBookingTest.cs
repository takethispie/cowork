using System;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.RoomBooking;
using cowork.usecases.RoomBooking.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.RoomBookingTests {

    [TestFixture]
    public class CreateRoomBookingTest {

        [Test]
        public void ShouldCreateRoombooking() {

            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 0, 0, 0);
            var input = new CreateRoomBookingInput(date, date.AddHours(1), 0, 0);
            var domain = new RoomBooking(input.Start, input.End, input.RoomId, input.ClientId);
            var timeSlotRepo = new Mock<ITimeSlotRepository>();
            timeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> {
                new TimeSlot(DayOfWeek.Wednesday, 8, 0, 20, 0, 0)
            });
            
            var roomRepo = new Mock<IRoomRepository>();
            roomRepo.Setup(m => m.GetById(0)).Returns(new Room(0, "test", RoomType.Call));
            
            var roomBookingRepo = new Mock<IRoomBookingRepository>();
            roomBookingRepo.Setup(m => m.Create(domain)).Returns(0);

            var res =
                new CreateRoomBooking(roomBookingRepo.Object, timeSlotRepo.Object, roomRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }


        [Test]
        public void ShouldFailOtherRoomBookedAtSameTime() {

            var date = new DateTime(2019, 10, 2, 10, 0,0);
            var input = new CreateRoomBookingInput(date, date.AddHours(1), 0, 0);
            var domain = new RoomBooking(input.Start, input.End, input.RoomId, input.ClientId);
            var timeSlotRepo = new Mock<ITimeSlotRepository>();
            timeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> {
                new TimeSlot(DayOfWeek.Wednesday, 8, 0, 20, 0, 0)
            });
            
            var roomRepo = new Mock<IRoomRepository>();
            roomRepo.Setup(m => m.GetById(0)).Returns(new Room(0, "test", RoomType.Call));
            
            var roomBookingRepo = new Mock<IRoomBookingRepository>();
            roomBookingRepo.Setup(m => m.Create(domain)).Returns(0);
            roomBookingRepo.Setup(m => m.GetAllFromGivenDate(date.Date)).Returns(new List<RoomBooking> {
                new RoomBooking(1, date, date.AddHours(2), 1, 0)
            });

            var res =
                new CreateRoomBooking(roomBookingRepo.Object, timeSlotRepo.Object, roomRepo.Object, input).Execute();
            Assert.AreEqual(-1, res);
        }
        
        
        [Test]
        public void ShouldFailRoomAlreadyBooked() {

            var date = new DateTime(2019, 10, 2, 10, 0,0);
            var input = new CreateRoomBookingInput(date, date.AddHours(1), 0, 0);
            var domain = new RoomBooking(input.Start, input.End, input.RoomId, input.ClientId);
            var timeSlotRepo = new Mock<ITimeSlotRepository>();
            timeSlotRepo.Setup(m => m.GetAllOfPlace(0)).Returns(new List<TimeSlot> {
                new TimeSlot(DayOfWeek.Wednesday, 8, 0, 20, 0, 0)
            });
            
            var roomRepo = new Mock<IRoomRepository>();
            roomRepo.Setup(m => m.GetById(0)).Returns(new Room(0, "test", RoomType.Call));
            
            var roomBookingRepo = new Mock<IRoomBookingRepository>();
            roomBookingRepo.Setup(m => m.Create(domain)).Returns(0);
            roomBookingRepo.Setup(m => m.GetAllFromGivenDate(date.Date)).Returns(new List<RoomBooking> {
                new RoomBooking(1, date, date.AddHours(2), 0, 1)
            });

            var res =
                new CreateRoomBooking(roomBookingRepo.Object, timeSlotRepo.Object, roomRepo.Object, input).Execute();
            Assert.AreEqual(-1, res);
        }
        

    }

}