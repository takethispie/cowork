using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class SubscriptionTypeDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemorySubscriptionTypeRepository();
            var subType = new SubscriptionType(-1, "test", -1, 5, 2, 20, 18, -1, 200, "Test description");
            subtypeId = repo.Create(subType);
        }


        private long subtypeId;
        private ISubscriptionTypeRepository repo;


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