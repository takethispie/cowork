using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class StaffLocationDbTest {

        [SetUp]
        public void Setup() {
            var user = new User(-1, "staff", "staff", "staff@staff.com", true, UserType.Staff);
            userId = userRepository.Create(user);
            var place = new Place(-1, "stafflocationtest", true, true, true, 10, 10, 50);
            placeId = placeRepository.Create(place);
            var staffLocation = new StaffLocation(-1, userId, placeId);
            staffLocationId = staffLocationRepository.Create(staffLocation);
        }


        [TearDown]
        public void TearDown() {
            staffLocationRepository.Delete(staffLocationId);
            placeRepository.DeleteById(placeId);
            userRepository.DeleteById(userId);
        }


        private IStaffLocationRepository staffLocationRepository;
        private IUserRepository userRepository;
        private IPlaceRepository placeRepository;
        private long userId, placeId, staffLocationId;
        private string connection;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            userRepository = new UserRepository(connection);
            staffLocationRepository = new StaffLocationRepository(connection);
            placeRepository = new PlaceRepository(connection);
        }


        [Test]
        public void Create() {
            var user = new User(-1, "staff2", "staff2", "staff2@staff.com", true, UserType.Staff);
            var newUser = userRepository.Create(user);
            var location = new StaffLocation(-1, newUser, placeId);
            var result = staffLocationRepository.Create(location);
            Assert.Greater(result, -1);
            staffLocationRepository.Delete(result);
            userRepository.DeleteById(newUser);
        }


        [Test]
        public void GetAll() {
            var result = staffLocationRepository.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = staffLocationRepository.GetById(staffLocationId);
            Assert.NotNull(result);
            Assert.AreEqual(placeId, result.PlaceId);
            Assert.AreEqual(userId, result.UserId);
        }


        [Test]
        public void GetByPlace() {
            var result = staffLocationRepository.GetAllByPlace(placeId);
            Assert.NotNull(result);
            Assert.Greater(result.Count, 0);
            Assert.AreEqual(placeId, result[0].PlaceId);
            Assert.AreEqual(userId, result[0].UserId);
        }


        [Test]
        public void Update() {
            var user = new User(-1, "staff3", "staff3", "staff3@staff.com", true, UserType.Staff);
            var newUser = userRepository.Create(user);
            var current = staffLocationRepository.GetById(staffLocationId);
            Assert.AreEqual(userId, current.UserId);

            current.UserId = newUser;
            var modifiedId = staffLocationRepository.Update(current);
            Assert.AreEqual(current.Id, modifiedId);
            var modified = staffLocationRepository.GetById(modifiedId);
            Assert.AreEqual(newUser, modified.UserId);
            staffLocationRepository.Delete(modifiedId);
            userRepository.DeleteById(newUser);
        }

    }

}