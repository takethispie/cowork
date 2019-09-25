using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class MealBookingDbTest {

        [SetUp]
        public void Setup() {
            placeId = placeRepo.Create(new Place(-1, "test", true, true, true, 1, 0, 0));
            date = DateTime.Today;
            mealId = mealRepo.Create(new Meal(-1, date, "salade tomate", placeId));
            userId = userRepo.Create(new User(-1, "Alexandre", "Felix", false, UserType.User));
            mealResId = repo.Create(new MealBooking(-1, mealId, userId, ""));
        }


        [TearDown]
        public void TearDown() {
            repo.Delete(mealResId);
            mealRepo.Delete(mealId);
            userRepo.DeleteById(userId);
            placeRepo.Delete(placeId);
        }


        private long mealResId, mealId, userId, placeId;
        private IMealBookingRepository repo;
        private IMealRepository mealRepo;
        private IUserRepository userRepo;
        private IPlaceRepository placeRepo;
        private string connection;
        private DateTime date;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            mealRepo = new MealRepository(connection);
            placeRepo = new PlaceRepository(connection);
            userRepo = new UserRepository(connection);
            repo = new MealBookingRepository(connection);
        }


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
        public void GetAllFromDateAndPlace() {
            var result = repo.GetAllFromDateAndPlace(date, placeId);
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