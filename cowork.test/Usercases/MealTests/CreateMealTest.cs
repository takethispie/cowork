using System;
using cowork.domain.Interfaces;
using cowork.usecases.Meal;
using cowork.usecases.Meal.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.MealTests {

    [TestFixture]
    public class CreateMealTest {

        [Test]
        public void ShouldCreateMeal() {
            var mockMealRepo = new Mock<IMealRepository>();
            mockMealRepo.Setup(m => m.Create(It.IsAny<domain.Meal>())).Returns(0);
            var date = DateTime.Now;
            var input = new MealInput(date, "patates", 0);
            var create = new CreateMeal(mockMealRepo.Object, input);
            var res = create.Execute();
            Assert.AreEqual(0, res);
        }

    }

}