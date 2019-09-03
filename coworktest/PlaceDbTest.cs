using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class PlaceDbTest {
        
        private IPlaceRepository repo;
        private string connection;
        private long placeId;

        [SetUp]
        public void Setup() {
            placeId = repo.Create(new Place(-1, "test", true, true, true, 1, 0, 1));
        }


        [OneTimeSetUp]
        public void OneTimeSetUp() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new PlaceRepository(connection);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void Create() {
            var newPlace = new Place(-1, "test2", true, false, true, 3, 3, 25);
            var result = repo.Create(newPlace);
            Assert.AreEqual(true, result > -1);
            Assert.NotNull(repo.GetByName("test2"));
            repo.DeleteById(result);
        }


        [Test]
        public void Delete() {
            var result = repo.DeleteByName("test");
            Assert.AreEqual(true, result);
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
            var result = repo.Update(current) > -1;
            Assert.AreEqual(true, result);
        }


        [TearDown]
        public void TearDown() {
            repo.DeleteById(placeId);
        }
    }

}