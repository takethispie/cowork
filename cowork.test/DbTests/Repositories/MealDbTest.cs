using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class MealDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryMealRepository();
            placeRepo = new InMemoryPlaceRepository();
            placeId = placeRepo.Create(new Place(-1, "test", true, true, true, 1, 0, 0));
            date = DateTime.Today;
            mealId = repo.Create(new Meal(-1, date, "", placeId));
        }
        
        
        private IMealRepository repo;
        private IPlaceRepository placeRepo;
        private long mealId, placeId;
        private string connection;
        private DateTime date;

        [Test]
        public void Create() {
            var newId = repo.Create(new Meal(-1, date, "test", placeId));
            Assert.Greater(newId, -1);
            var added = repo.GetById(newId);
            Assert.NotNull(added);
            Assert.AreEqual("test", added.Description);
            repo.Delete(newId);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(mealId);
            Assert.IsTrue(result);
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
        public void GetAllFromPlace() {
            var result = repo.GetAllFromPlace(placeId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(mealId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(mealId);
            Assert.NotNull(current);
            var newDate = DateTime.Today.AddDays(1);
            current.Date = newDate;
            var id = repo.Update(current);
            var modified = repo.GetById(id);
            Assert.NotNull(modified);
            Assert.AreEqual(newDate, modified.Date);
        }

    }

}