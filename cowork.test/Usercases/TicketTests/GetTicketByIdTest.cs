using System;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.TicketTests {

    [TestFixture]
    public class GetTicketByIdTest {

        [Test]
        public void ShouldGetTicketById() {
            var domain = new Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.GetById(0)).Returns(domain);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(1)).Returns(new TicketAttribution());

            var mockTicketComRepo = new Mock<ITicketCommentRepository>();
            mockTicketComRepo.Setup(m => m.GetByTicketId(0)).Returns(new List<TicketComment>());
            
            var mockUserRepo = new Mock<IUserRepository>();
            
            var mockTicketWareRepo = new Mock<ITicketWareRepository>();
            mockTicketWareRepo.Setup(m => m.GetByTicketId(1)).Returns(new TicketWare());

            var res = new GetTicketById(mockTicketRepo.Object, mockTicketAttrRepo.Object, mockUserRepo.Object,
                mockTicketComRepo.Object, 0).Execute();
            Assert.AreEqual(0, res.Id);
            Assert.AreEqual(TicketState.Open, res.State);
            Assert.AreEqual("test", res.Description);
            Assert.AreEqual("problem", res.Title);
        }
        
        
        [Test]
        public void ShouldFailGettingTicketById() {
            var domain = new Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.GetById(1)).Returns(domain);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(1)).Returns(new TicketAttribution());

            var mockTicketComRepo = new Mock<ITicketCommentRepository>();
            mockTicketComRepo.Setup(m => m.GetByTicketId(0)).Returns(new List<TicketComment>());
            
            var mockUserRepo = new Mock<IUserRepository>();
            
            var mockTicketWareRepo = new Mock<ITicketWareRepository>();
            mockTicketWareRepo.Setup(m => m.GetByTicketId(1)).Returns(new TicketWare());

            var res = new GetTicketById(mockTicketRepo.Object, mockTicketAttrRepo.Object, mockUserRepo.Object,
                mockTicketComRepo.Object, 0).Execute();
            Assert.IsNull(res);
        }

    }

}