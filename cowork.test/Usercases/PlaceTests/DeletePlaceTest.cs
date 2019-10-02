using cowork.domain.Interfaces;
using cowork.usecases.Place;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.PlaceTests {

    [TestFixture]
    public class DeletePlaceTest {

        [Test]
        public void ShouldDeletePlace() {
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Delete(0)).Returns(true);
            var res = new DeletePlace(mockPlaceRepo.Object, 0).Execute();
            Assert.IsTrue(res);
        }
        
        
        [Test]
        public void ShouldFailDeletingPlace() {
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Delete(0)).Returns(false);
            var res = new DeletePlace(mockPlaceRepo.Object, 0).Execute();
            Assert.IsFalse(res);
        }

    }

}