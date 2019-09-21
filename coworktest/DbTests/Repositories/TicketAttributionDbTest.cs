using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class TicketAttributionDbTest {

        [SetUp]
        public void Setup() {
            var ticketAttribution = new TicketAttribution(-1, ticketId, staffId);
            ticketAttrId = ticketAttributionRepository.Create(ticketAttribution);
        }


        [TearDown]
        public void TearDown() {
            ticketAttributionRepository.Delete(ticketAttrId);
        }


        private string connection;
        private long ticketId, staffId, userId, placeId, wareId, ticketAttrId;
        private ITicketRepository ticketRepository;
        private ITicketAttributionRepository ticketAttributionRepository;
        private IUserRepository userRepository;
        private IWareRepository wareRepository;
        private IPlaceRepository placeRepository;
        private User user;
        private User staff;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            ticketRepository = new TicketRepository(connection);
            ticketAttributionRepository = new TicketAttributionRepository(connection);
            userRepository = new UserRepository(connection);
            wareRepository = new WareRepository(connection);
            placeRepository = new PlaceRepository(connection);

            var place = new Place(-1, "testticket", false, true, true, 3, 1, 20);
            placeId = placeRepository.Create(place);
            staff = new User(-1, "alexandre", "felix", false, UserType.Staff);
            staffId = userRepository.Create(staff);
            user = new User(-1, "Alexandre", "Felix", false, UserType.User);
            userId = userRepository.Create(user);
            var ware = new Ware(-1, "Dell T1800", "Ordinateur de bureau", "1322000573-13242142-n1235v", placeId, false);
            wareId = wareRepository.Create(ware);
            var ticket = new Ticket(-1, userId, TicketState.New, "", placeId, "test", DateTime.Now);
            ticketId = ticketRepository.Create(ticket);
        }


        [OneTimeTearDown]
        public void OneTimeTearDown() {
            ticketRepository.Delete(ticketId);
            wareRepository.Delete(wareId);
            userRepository.DeleteById(userId);
            userRepository.DeleteById(staffId);
            placeRepository.DeleteById(placeId);
        }


        [Test]
        public void Create() {
            ticketAttributionRepository.Delete(ticketAttrId);
            var ticket = new TicketAttribution(-1, ticketId, staffId);
            var id = ticketAttributionRepository.Create(ticket);
            Assert.Greater(id, -1);
            var added = ticketAttributionRepository.GetById(id);
            Assert.AreEqual(ticketId, added.TicketId);
            Assert.AreEqual(staffId, added.StaffId);
            ticketAttributionRepository.Delete(id);
        }


        [Test]
        public void Delete() {
            var result = ticketAttributionRepository.Delete(ticketAttrId);
            Assert.IsTrue(result);
        }


        [Test]
        public void GetAll() {
            var result = ticketAttributionRepository.GetAll();
            Assert.Greater(result.Count, 0);
        }


        [Test]
        public void GetByAttributedTo() {
            var result = ticketAttributionRepository.GetAllFromStaffId(staffId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = ticketAttributionRepository.GetById(ticketAttrId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() { }

    }

}