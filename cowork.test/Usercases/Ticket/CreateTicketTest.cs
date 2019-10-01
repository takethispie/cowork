using System;
using cowork.domain;
using cowork.domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Ticket {

    [TestFixture]
    public class CreateTicketTest {

        [Test]
        public void ShouldCreateTicket() {
            var dateTime = DateTime.Now;
            var input = new domain.Ticket(0, TicketState.Open, "test", 0, "problem", DateTime.Now);
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Create(input)).Returns(0);
            
        }

    }

}