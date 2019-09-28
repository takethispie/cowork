using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class WareDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryWareRepository();
            placeRepository = new InMemoryPlaceRepository();
            bookingRepository = new InMemoryWareBookingRepository();
            userRepository = new InMemoryUserRepository();
            var place = new Place(-1, "testware", true, true, true, 3, 1, 30);
            placeId = placeRepository.Create(place);
            var ware = new Ware(-1, "Dell T1600", "Ordinateur de bureau", "132251573-13242142-n1235v", placeId, false);
            wareId = repo.Create(ware);
            userId = userRepository.Create(new User(-1, "Alexandre", "testwarebooking", false,
                UserType.User));
            wareBookingId =
                bookingRepository.Create(new WareBooking(-1, userId, wareId, DateTime.Now, DateTime.Now.AddHours(1)));
        }


        private IWareRepository repo;
        private IPlaceRepository placeRepository;
        private IWareBookingRepository bookingRepository;
        private IUserRepository userRepository;
        private long placeId, wareId, userId, wareBookingId;


        [Test]
        public void Create() {
            var ware = new Ware(-1, "Dell T2600", "workstation de bureau", "1322kjkj573-13242142-n1287z", placeId,
                false);
            var result = repo.Create(ware);
            Assert.IsTrue(result > -1);
            repo.Delete(result);
        }


        [Test]
        public void Delete() {
            bookingRepository.Delete(wareBookingId);
            var result = repo.Delete(wareId);
            Assert.IsTrue(result);
            Assert.IsNull(repo.GetById(wareId));
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllBooking() {
            var result = bookingRepository.GetAll();
            Assert.NotNull(result);
            Assert.AreEqual(userId, result[0].UserId);
        }


        [Test]
        public void GetAllFromPlace() {
            var result = repo.GetAllFromPlace(placeId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(wareId);
            Assert.NotNull(result);
            Assert.AreEqual("Dell T1600", result.Name);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(wareId);
            Assert.NotNull(current);
            current.InStorage = true;
            var newId = repo.Update(current);
            Assert.Greater(newId, -1);
            var modified = repo.GetById(newId);
            Assert.NotNull(modified);
            Assert.IsTrue(modified.InStorage);
        }

    }

}