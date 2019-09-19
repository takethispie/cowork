using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class SubscriptionTypeDbTest {

        [SetUp]
        public void Setup() {
            var subType = new SubscriptionType(-1, "test", -1, 5, 2, 20, 18, -1, 200, "Test description");
            subtypeId = repo.Create(subType);
        }


        [TearDown]
        public void TearDown() {
            repo.Delete(subtypeId);
        }


        private string connection;
        private long subtypeId;
        private ISubscriptionTypeRepository repo;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new SubscriptionTypeRepository(connection);
        }


        [Test]
        public void Create() {
            var subType = new SubscriptionType(-1, "test2", -1, 5, 2, 20, 18, -1, 200, "test description");
            var result = repo.Create(subType);
            Assert.IsTrue(result > -1);
            repo.Delete(result);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(subtypeId);
            Assert.IsTrue(result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(subtypeId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetByName() {
            var result = repo.GetByName("test");
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(subtypeId);
            Assert.NotNull(current);
            Assert.AreEqual(5, current.PriceFirstHour);
            current.PriceFirstHour = 10;
            var result = repo.Update(current);
            Assert.IsTrue(result > -1);
            var modified = repo.GetById(result);
            Assert.AreEqual(10, modified.PriceFirstHour);
        }

    }

}