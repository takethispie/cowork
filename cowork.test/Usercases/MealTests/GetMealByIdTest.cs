using System;
using cowork.domain.Interfaces;
using cowork.usecases.Meal;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.MealTests {

    [TestFixture]
    public class GetMealByIdTest {

        [Test]
        public void ShouldFindMealById() {
            var mockMealRepo = new Mock<IMealRepository>();
            var date = DateTime.Now;
            var meal = new domain.Meal(date, "patates", 0);
            mockMealRepo.Setup(m => m.GetById(0)).Returns(meal);
            var getMealById = new GetMealById(mockMealRepo.Object, 0);
            var res = getMealById.Execute();
            Assert.NotNull(res);
            Assert.AreEqual("patates", res.Description);
            Assert.AreEqual(date, res.Date);
        }
        
        [Test]
        public void ShouldFailFindingMealById() {
            var mockMealRepo = new Mock<IMealRepository>();
            var date = DateTime.Now;
            var meal = new domain.Meal(date, "patates", 0);
            mockMealRepo.Setup(m => m.GetById(0)).Returns(meal);
            var getMealById = new GetMealById(mockMealRepo.Object, 1);
            var res = getMealById.Execute();
            Assert.IsNull(res);
        }


    }

}