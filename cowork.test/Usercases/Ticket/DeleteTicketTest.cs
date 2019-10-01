using System;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using cowork.usecases.Ticket.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Ticket {

    [TestFixture]
    public class DeleteTicketTest {

        [Test]
        public void ShouldDeleteTicket() {
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Delete(0)).Returns(true);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(1)).Returns(new TicketAttribution());

            var mockTicketComRepo = new Mock<ITicketCommentRepository>();
            mockTicketComRepo.Setup(m => m.GetByTicketId(0)).Returns(new List<TicketComment>());
            
            var mockTicketWareRepo = new Mock<ITicketWareRepository>();
            mockTicketWareRepo.Setup(m => m.GetByTicketId(1)).Returns(new TicketWare());

            var res = new DeleteTicket(mockTicketRepo.Object, mockTicketComRepo.Object, mockTicketAttrRepo.Object,
                mockTicketWareRepo.Object, 0).Execute();
            Assert.AreEqual(true, res);
        }
        
        
        [Test]
        public void ShouldFailDeletingTicket() {
            var mockTicketRepo = new Mock<ITicketRepository>();
            mockTicketRepo.Setup(m => m.Delete(0)).Returns(false);
            
            var mockTicketAttrRepo = new Mock<ITicketAttributionRepository>();
            mockTicketAttrRepo.Setup(m => m.GetFromTicket(1)).Returns(new TicketAttribution());

            var mockTicketComRepo = new Mock<ITicketCommentRepository>();
            mockTicketComRepo.Setup(m => m.GetByTicketId(0)).Returns(new List<TicketComment>());
            
            var mockTicketWareRepo = new Mock<ITicketWareRepository>();
            mockTicketWareRepo.Setup(m => m.GetByTicketId(1)).Returns(new TicketWare());

            var res = new DeleteTicket(mockTicketRepo.Object, mockTicketComRepo.Object, mockTicketAttrRepo.Object,
                mockTicketWareRepo.Object, 0).Execute();
            Assert.AreEqual(false, res);
        }

    }

}