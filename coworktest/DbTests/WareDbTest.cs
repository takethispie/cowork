using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class WareDbTest {

        [SetUp]
        public void Setup() {
            var place = new Place(-1, "testware", true, true, true, 3, 1, 30);
            placeId = placeRepository.Create(place);
            var ware = new Ware(-1, "Dell T1600", "Ordinateur de bureau", "132251573-13242142-n1235v", placeId, false);
            wareId = repo.Create(ware);
            userId = userRepository.Create(new User(-1, "Alexandre", "testwarebooking", "warebooking@test.com", false,
                UserType.User));
            wareBookingId =
                bookingRepository.Create(new WareBooking(-1, userId, wareId, DateTime.Now, DateTime.Now.AddHours(1)));
        }


        [TearDown]
        public void TearDown() {
            bookingRepository.Delete(wareBookingId);
            repo.Delete(wareId);
            placeRepository.DeleteById(placeId);
            userRepository.DeleteById(userId);
        }


        private IWareRepository repo;
        private IPlaceRepository placeRepository;
        private IWareBookingRepository bookingRepository;
        private IUserRepository userRepository;
        private string connection;
        private long placeId, wareId, userId, wareBookingId;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new WareRepository(connection);
            placeRepository = new PlaceRepository(connection);
            bookingRepository = new WareBookingRepository(connection);
            userRepository = new UserRepository(connection);
        }


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