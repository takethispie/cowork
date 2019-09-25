using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Repositories;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class SubscriptionDbTest {

        [SetUp]
        public void Setup() {
            var subType = new SubscriptionType(-1, "test", -1, 5, 2, 20, 18, -1, 200, "test description");
            subTypeId = typeRepo.Create(subType);
            var place = new Place(-1, "test", false, false, false, 3, 3, 3);
            placeId = placeRepo.Create(place);
            place.Id = placeId;
            var user = new User(-1, "alexandre", "felix", false, UserType.User);
            userId = userRepo.Create(user);
            user.Id = userId;
            var sub = new Subscription(-1, subTypeId, userId, DateTime.Now, placeId, false);
            subId = repo.Create(sub);
        }


        [TearDown]
        public void Teardown() {
            repo.Delete(subId);
            typeRepo.Delete(subTypeId);
            placeRepo.Delete(placeId);
            userRepo.DeleteById(userId);
        }


        private ISubscriptionRepository repo;
        private ISubscriptionTypeRepository typeRepo;
        private IUserRepository userRepo;
        private IPlaceRepository placeRepo;
        private long subId, subTypeId, placeId, userId;
        private string connection;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new InMemorySubscriptionRepository();
            typeRepo = new InMemorySubscriptionTypeRepository();
            userRepo = new InMemoryUserRepository();
            placeRepo = new InMemoryPlaceRepository();
        }


        [Test]
        public void Create() {
            var user = new User(-1, "jean", "jean", true, UserType.User);
            var userid = userRepo.Create(user);
            var sub = new Subscription(-1, subTypeId, userid, DateTime.Now, placeId, false);
            var result = repo.Create(sub);
            Assert.Greater(result, -1);
            repo.Delete(result);
            userRepo.DeleteById(userid);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(subId);
            Assert.IsTrue(result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(subId);
            Assert.NotNull(result);
            Assert.AreEqual(userId, result.ClientId);
        }


        [Test]
        public void GetOfUser() {
            var result = repo.GetOfUser(userId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(subId);
            Assert.NotNull(current);
            //DateTime.Now.Date avoid milliseconds mismatch between equals date
            var newDate = DateTime.Now.Date;
            Assert.AreNotEqual(current.LatestRenewal, newDate);
            current.LatestRenewal = newDate;
            var result = repo.Update(current);
            Assert.Greater(result, -1);
            var modified = repo.GetById(result);
            Assert.AreEqual(newDate.Date, modified.LatestRenewal.Date);
        }

    }

}