using cowork.domain.Interfaces;
using cowork.usecases.Meal;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.MealTests {

    [TestFixture]
    public class DeleteMealTest {

        [Test]
        public void ShouldDeleteMeal() {
            var mockMealRepo = new Mock<IMealRepository>();
            mockMealRepo.Setup(m => m.Delete(0)).Returns(true);

            var delete = new DeleteMeal(mockMealRepo.Object, 0).Execute();
            Assert.IsTrue(delete);
        }
        
        
        [Test]
        public void ShouldFailDeletingNotExistingMeal() {
            var mockMealRepo = new Mock<IMealRepository>();
            mockMealRepo.Setup(m => m.Delete(0)).Returns(false);

            var delete = new DeleteMeal(mockMealRepo.Object, 0).Execute();
            Assert.IsFalse(delete);
        }

    }

}