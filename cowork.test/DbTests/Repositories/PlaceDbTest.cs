using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class PlaceDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryPlaceRepository();
            placeId = repo.Create(new Place(-1, "test", true, true, true, 1, 0, 1));
        }


        private IPlaceRepository repo;
        private long placeId;


        [Test]
        public void Create() {
            var newPlace = new Place(-1, "test2", true, false, true, 3, 3, 25);
            var result = repo.Create(newPlace);
            Assert.AreEqual(true, result > -1);
            Assert.NotNull(repo.GetByName("test2"));
            repo.Delete(result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetByName() {
            var result = repo.GetByName("test");
            Assert.NotNull(result);
            Assert.AreEqual("test", result.Name);
            Assert.AreEqual(true, result.UnlimitedBeverages);
        }


        [Test]
        public void Update() {
            var current = repo.GetByName("test");
            Assert.AreEqual(true, current.HighBandwidthWifi);
            current.HighBandwidthWifi = false;
            var result = repo.Update(current);
            Assert.AreEqual(true, result > -1);
        }

    }

}