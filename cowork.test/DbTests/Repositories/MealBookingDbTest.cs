using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class MealBookingDbTest {

        [SetUp]
        public void Setup() {
            mealRepo = new InMemoryMealRepository();
            placeRepo = new InMemoryPlaceRepository();
            userRepo = new InMemoryUserRepository();
            repo = new InMemoryMealBookingRepository();
            placeId = placeRepo.Create(new Place(-1, "test", true, true, true, 1, 0, 0));
            date = DateTime.Today;
            mealId = mealRepo.Create(new Meal(-1, date, "salade tomate", placeId));
            userId = userRepo.Create(new User(-1, "Alexandre", "Felix", false, UserType.User));
            mealResId = repo.Create(new MealBooking(-1, mealId, userId, ""));
        }


        private long mealResId, mealId, userId, placeId;
        private IMealBookingRepository repo;
        private IMealRepository mealRepo;
        private IUserRepository userRepo;
        private IPlaceRepository placeRepo;
        private DateTime date;


        [Test]
        public void Create() {
            var id = repo.Create(new MealBooking(-1, mealId, userId, ""));
            Assert.Greater(id, -1);
            var result = repo.GetById(id);
            Assert.NotNull(result);
            repo.Delete(id);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(mealResId);
            Assert.IsTrue(result);
            var shouldBeNull = repo.GetById(mealResId);
            Assert.IsNull(shouldBeNull);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllFromUser() {
            var result = repo.GetAllFromUser(userId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(mealResId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(mealResId);
            Assert.NotNull(current);
            current.Note = "testtest";
            var id = repo.Update(current);
            Assert.Greater(id, -1);
            var modified = repo.GetById(id);
            Assert.NotNull(modified);
            Assert.AreEqual("testtest", modified.Note);
        }

    }

}