using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using cowork.usecases.Ticket.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.TicketTests {

    [TestFixture]
    public class UpdateTicketTest {

        [Test]
        public void ShouldUpdateTicket() {
            var domain = new Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var input = new UpdateTicketInput(0, 0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Update(domain)).Returns(0);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(0)).Returns(new TicketAttribution(0, 0, 0));

            var res = new UpdateTicket(mockTicketRepo.Object, mockTicketAttrRepo.Object, input, 0).Execute();
            Assert.AreEqual(0, res);
        }
        
        
        [Test]
        public void ShouldFailUpdatingTicket() {
            var domain = new Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var input = new UpdateTicketInput(0, 0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Update(domain)).Returns(0);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(1)).Returns(new TicketAttribution(0, 0, 0));

            var res = new UpdateTicket(mockTicketRepo.Object, mockTicketAttrRepo.Object, input, 0).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}