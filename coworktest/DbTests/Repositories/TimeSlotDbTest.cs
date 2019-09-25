using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class TimeSlotDbTest {

        [SetUp]
        public void Setup() {
            placeId = placeRepo.Create(new Place(-1, "test", true, true, true, 1, 0, 1));
            var slot = new TimeSlot(-1, DayOfWeek.Monday, 8, 30, 19, 30, placeId);
            slotId = repo.Create(slot);
        }


        [TearDown]
        public void TearDown() {
            repo.Delete(slotId);
            placeRepo.Delete(placeId);
        }


        private ITimeSlotRepository repo;
        private IPlaceRepository placeRepo;
        private string connection;
        private long slotId, placeId;


        [OneTimeSetUp]
        public void OneTimeSetUp() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new TimeSlotRepository(connection);
            placeRepo = new PlaceRepository(connection);
        }


        [Test]
        public void Create() {
            var slot = new TimeSlot(-1, DayOfWeek.Monday, 8, 30, 19, 30, placeId);
            var result = repo.Create(slot);
            Assert.Greater(result, -1);
            repo.Delete(result);
        }


        [Test]
        public void Delete() {
            var slot = new TimeSlot(-1, DayOfWeek.Monday, 8, 30, 19, 30, placeId);
            var result = repo.Create(slot);
            Assert.Greater(result, -1);
            var success = repo.Delete(result);
            Assert.AreEqual(true, success);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(slotId);
            Assert.NotNull(result);
            Assert.AreEqual(8, result.StartHour);
            Assert.AreEqual(30, result.StartMinutes);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(slotId);
            Assert.NotNull(current);
            current.Day = DayOfWeek.Sunday;
            var result = repo.Update(current);
            Assert.Greater(result, -1);
            var modified = repo.GetById(slotId);
            Assert.NotNull(modified);
            Assert.AreEqual(DayOfWeek.Sunday, modified.Day);
        }

    }

}