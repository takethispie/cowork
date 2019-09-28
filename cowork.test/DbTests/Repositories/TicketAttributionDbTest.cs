using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class TicketAttributionDbTest {

        [SetUp]
        public void Setup() {
            ticketRepository = new InMemoryTicketRepository();
            ticketAttributionRepository = new InMemoryTicketAttributionRepository();
            userRepository = new InMemoryUserRepository();
            wareRepository = new InMemoryWareRepository();
            placeRepository = new InMemoryPlaceRepository();

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
            var ticketAttribution = new TicketAttribution(-1, ticketId, staffId);
            ticketAttrId = ticketAttributionRepository.Create(ticketAttribution);
        }


        private long ticketId, staffId, userId, placeId, wareId, ticketAttrId;
        private ITicketRepository ticketRepository;
        private ITicketAttributionRepository ticketAttributionRepository;
        private IUserRepository userRepository;
        private IWareRepository wareRepository;
        private IPlaceRepository placeRepository;
        private User user;
        private User staff;


        [OneTimeTearDown]
        public void OneTimeTearDown() {
            ticketRepository.Delete(ticketId);
            wareRepository.Delete(wareId);
            userRepository.DeleteById(userId);
            userRepository.DeleteById(staffId);
            placeRepository.Delete(placeId);
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