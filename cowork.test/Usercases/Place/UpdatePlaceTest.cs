using cowork.domain.Interfaces;
using cowork.usecases.Place;
using cowork.usecases.Place.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Place {

    [TestFixture]
    public class UpdatePlaceTest {

        [Test]
        public void ShouldDeletePlace() {
            var place = new domain.Place("test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Update(place)).Returns(0);
            var res = new UpdatePlace(mockPlaceRepo.Object, place).Execute();
            Assert.AreEqual(0, res);
        }
        
        
        [Test]
        public void ShouldFailDeletingPlace() {
            var place = new domain.Place("test", true, true, true, 1, 2, 3);
            var mockPlaceRepo = new Mock<IPlaceRepository>();
            mockPlaceRepo.Setup(m => m.Update(It.IsAny<domain.Place>())).Returns(-1);
            var res = new UpdatePlace(mockPlaceRepo.Object, place).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}