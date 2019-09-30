using System;
using cowork.domain.Interfaces;
using cowork.usecases.Meal;
using cowork.usecases.Meal.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Meal {

    [TestFixture]
    public class UpdateMealTest {

        [Test]
        public void ShouldUpdateMeal() {
            var mockMealRepo = new Mock<IMealRepository>();
            mockMealRepo.Setup(m => m.Update(It.IsAny<domain.Meal>())).Returns(0);
            var date = DateTime.Now;
            var input = new domain.Meal(date, "patates", 0);
            var updateMeal = new UpdateMeal(mockMealRepo.Object, input);
            var res = updateMeal.Execute();
            Assert.AreEqual(0, res);
        }
        
        [Test]
        public void ShouldFailUpdatingMeal() {
            var mockMealRepo = new Mock<IMealRepository>();
            mockMealRepo.Setup(m => m.Update(It.IsAny<domain.Meal>())).Returns(-1);
            var date = DateTime.Now;
            var input = new domain.Meal(date, "patates", 0);
            var updateMeal = new UpdateMeal(mockMealRepo.Object, input);
            var res = updateMeal.Execute();
            Assert.AreEqual(-1, res);
        }

    }

}