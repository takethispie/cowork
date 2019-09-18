using System;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class TicketDbTest {

        [SetUp]
        public void Setup() {
            var place = new Place(-1, "testticket", false, true, true, 3, 1, 20);
            placeId = placeRepo.Create(place);
            staff = new User(-1, "ticket1user", "t", "test@test.com", false, UserType.User);
            persId = userRepo.Create(staff);
            user = new User(-1, "ticket2user", "t", "test@test.com", false, UserType.User);
            userId = userRepo.Create(user);
            var ware = new Ware(-1, "Dell T1600", "Ordinateur de bureau", "132251573-13242142-n1235v", placeId, false);
            wareId = wareRepo.Create(ware);
            var ticket = new Ticket(-1, userId, TicketState.New, "", placeId, "test", DateTime.Now);
            ticketId = repo.Create(ticket);
            var ticketWare = new TicketWare(-1, ticketId, wareId);
            ticketWareId = ticketWareRepository.Create(ticketWare);
        }


        [TearDown]
        public void TearDown() {
            ticketWareRepository.Delete(ticketWareId);
            ticketCommentRepository.Delete(commentId);
            repo.Delete(ticketId);
            wareRepo.Delete(wareId);
            userRepo.DeleteById(userId);
            userRepo.DeleteById(persId);
            placeRepo.DeleteById(placeId);
        }


        private ITicketRepository repo;
        private IUserRepository userRepo;
        private IWareRepository wareRepo;
        private IPlaceRepository placeRepo;
        private ITicketWareRepository ticketWareRepository;

        private string connection;
        private long ticketId, persId, userId, placeId, wareId, commentId, ticketWareId;
        private User user;
        private User staff;
        private TicketCommentRepository ticketCommentRepository;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new TicketRepository(connection);
            userRepo = new UserRepository(connection);
            placeRepo = new PlaceRepository(connection);
            wareRepo = new WareRepository(connection);
            ticketCommentRepository = new TicketCommentRepository(connection);
            ticketWareRepository = new TicketWareRepository(connection);
        }


        [Test]
        public void Create() {
            var ticket = new Ticket(-1, userId, TicketState.New, "test", placeId, "test", DateTime.Now);
            var id = repo.Create(ticket);
            Assert.Greater(id, -1);
            var result = repo.GetById(id);
            Assert.NotNull(result);
            repo.Delete(id);
        }


        [Test]
        public void CreateAndDeleteComment() {
            var comment = new TicketComment(-1, "test", ticketId, userId, DateTime.Now);
            commentId = ticketCommentRepository.Create(comment);
            Assert.Greater(commentId, -1);
            var result = ticketCommentRepository.Delete(commentId);
            Assert.IsTrue(result);
        }


        [Test]
        public void CreateAndUpdateComment() {
            var comment = new TicketComment(-1, "test", ticketId, userId, DateTime.Now);
            commentId = ticketCommentRepository.Create(comment);
            Assert.Greater(commentId, -1);
            var added = ticketCommentRepository.GetById(commentId);
            Assert.AreEqual("test", added.Content);
            Assert.AreEqual(userId, comment.AuthorId);
            added.Content = "testupdated";
            var updated = ticketCommentRepository.Update(added);
            Assert.AreEqual(commentId, updated);
            var modified = ticketCommentRepository.GetById(commentId);
            Assert.AreEqual("testupdated", modified.Content);
        }


        [Test]
        public void CreateComment() {
            var comment = new TicketComment(-1, "test", ticketId, userId, DateTime.Now);
            commentId = ticketCommentRepository.Create(comment);
            Assert.Greater(commentId, -1);
        }


        [Test]
        public void Delete() {
            ticketWareRepository.Delete(ticketWareId);
            var result = repo.Delete(ticketId);
            Assert.IsTrue(result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllComments() {
            var result = ticketCommentRepository.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllOfPlace() {
            var result = repo.GetAllOfPlace(placeId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllOpenedBy() {
            var result = repo.GetAllOpenedBy(user);
            Assert.NotNull(result);
        }


        [Test]
        public void GetAllTicketWare() {
            var result = ticketWareRepository.GetAll();
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Ware.Name, "Dell T1600");
        }


        [Test]
        public void GetbyId() {
            var result = repo.GetById(ticketId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetByIdTicketWare() {
            var result = ticketWareRepository.GetById(ticketWareId);
            Assert.NotNull(result);
            Assert.AreEqual(result.Ware.Name, "Dell T1600");
        }


        [Test]
        public void GetByTicketIdTicketWare() {
            var result = ticketWareRepository.GetByTicketId(ticketId);
            Assert.NotNull(result);
            Assert.AreEqual(result.Ware.Name, "Dell T1600");
        }


        [Test]
        public void GetCommentByTicketId() {
            var comment = new TicketComment(-1, "test", ticketId, userId, DateTime.Now);
            commentId = ticketCommentRepository.Create(comment);
            Assert.Greater(commentId, -1);
            var result = ticketCommentRepository.GetByTicketId(ticketId);
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var current = repo.GetById(ticketId);
            Assert.NotNull(current);
            current.Description = "marche pas";
            var id = repo.Update(current);
            Assert.Greater(id, -1);
            var modified = repo.GetById(id);
            Assert.AreEqual(modified.Description, "marche pas");
        }

    }

}