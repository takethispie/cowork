using cowork.domain.Interfaces;
using cowork.usecases.Place;
using cowork.usecases.Place.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Place {

    [TestFixture]
    public class CreatePlaceTest {

        [Test]
        public void ShouldCreatePlace() {
            var place = new CreatePlaceInput("test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Create(It.IsAny<domain.Place>())).Returns(0);
            var res = new CreatePlace(mockPlaceRepo.Object, place).Execute();
            Assert.AreEqual(0, res);
        }
        
        [Test]
        public void ShouldFailCreatingPlace() {
            var place = new CreatePlaceInput("test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Create(It.IsAny<domain.Place>())).Returns(-1);
            var res = new CreatePlace(mockPlaceRepo.Object, place).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}