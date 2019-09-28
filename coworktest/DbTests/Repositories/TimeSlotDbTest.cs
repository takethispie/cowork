using System;
using cowork.domain;
using cowork.domain.Interfaces;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest.DbTests.Repositories {

    [TestFixture]
    public class TimeSlotDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryTimeSlotRepository();
            placeRepo = new InMemoryPlaceRepository();
            placeId = placeRepo.Create(new Place(-1, "test", true, true, true, 1, 0, 1));
            var slot = new TimeSlot(-1, DayOfWeek.Monday, 8, 30, 19, 30, placeId);
            slotId = repo.Create(slot);
        }


        private ITimeSlotRepository repo;
        private IPlaceRepository placeRepo;
        private long slotId, placeId;


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