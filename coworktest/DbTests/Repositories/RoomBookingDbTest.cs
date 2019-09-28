using System;
using cowork.domain;
using cowork.domain.Interfaces;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest.DbTests.Repositories {

    [TestFixture]
    public class RoomBookingDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryRoomBookingRepository();
            roomRepo = new InMemoryRoomRepository();
            userRepo = new InMemoryUserRepository();
            placeRepo = new InMemoryPlaceRepository();
            var place = new Place(-1, "test", false, false, false, 3, 3, 39);
            placeId = placeRepo.Create(place);
            place.Id = placeId;
            var room = new Room(-1, place.Id, "test", RoomType.Call);
            roomId = roomRepo.Create(room);
            room.Id = roomId;
            var user = new User(-1, "alexandre", "felix", false, UserType.User);
            userId = userRepo.Create(user);
            user.Id = userId;
            currentDate = DateTime.Now;
            roomBookingId = repo.Create(new RoomBooking(-1, currentDate, currentDate.AddHours(1), room.Id, user.Id));
        }


        private IRoomBookingRepository repo;
        private IRoomRepository roomRepo;
        private IUserRepository userRepo;
        private IPlaceRepository placeRepo;
        private long roomId, roomBookingId, userId, placeId;
        private DateTime currentDate;


        [OneTimeTearDown]
        public void OneTimeTearDown() {
            repo.Delete(roomBookingId);
            roomRepo.Delete(roomId);
            userRepo.DeleteById(userId);
            placeRepo.Delete(placeId);
        }


        [Test]
        public void Create() {
            var booking = new RoomBooking(-1, DateTime.Now, DateTime.Now.AddMinutes(30), roomId, userId);
            var result = repo.Create(booking);
            Assert.Greater(result, -1);
            repo.Delete(result);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(roomBookingId);
            Assert.IsTrue(result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllDate() {
            var result = repo.GetAllFromGivenDate(currentDate);
            Assert.NotNull(result);
            Assert.AreEqual(userId, result[0].ClientId);
        }


        [Test]
        public void GetAllOfUser() {
            var result = repo.GetAllOfUser(userId);
            Assert.NotNull(result);
            Assert.Greater(result.Count, 0);
            Assert.AreEqual(userId, result[0].ClientId);
            Assert.AreEqual(roomId, result[0].RoomId);
        }


        [Test]
        public void GetAllRoom() {
            var result = repo.GetAllOfRoom(roomId);
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(result[0].ClientId, userId);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(roomBookingId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(roomBookingId);
            current.End = current.End.AddMinutes(30);
            var result = repo.Update(current);
            Assert.Greater(result, -1);
            var modified = repo.GetById(roomBookingId);
            Assert.AreEqual(current.End, modified.End);
        }

    }

}