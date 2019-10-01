using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using cowork.usecases.Ticket.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Ticket {

    [TestFixture]
    public class CreateTicketTest {

        [Test]
        public void ShouldCreateTicket() {
            var domain = new domain.Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var input = new CreateTicketInput(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Create(domain)).Returns(0);

            var res = new CreateTicket(mockTicketRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }
        
        
        [Test]
        public void ShouldFailCreatingTicket() {
            var input = new CreateTicketInput(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Create(It.IsAny<domain.Ticket>())).Returns(-1);

            var res = new CreateTicket(mockTicketRepo.Object, input).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}