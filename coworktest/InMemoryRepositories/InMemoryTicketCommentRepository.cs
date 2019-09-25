using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketCommentRepository : ITicketCommentRepository {

        public List<TicketComment> TicketComments;


        public InMemoryTicketCommentRepository() {
            TicketComments = new List<TicketComment>();
        }
        
        public long Create(TicketComment ticketComment) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(TicketComment ticketComment) {
            throw new System.NotImplementedException();
        }


        public List<TicketComment> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<TicketComment> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public TicketComment GetById(long id) {
            throw new System.NotImplementedException();
        }


        public List<TicketComment> GetByTicketId(long ticketId) {
            throw new System.NotImplementedException();
        }


        public List<TicketComment> LastCommentsFromUser(long userId, int numberOfComments) {
            throw new System.NotImplementedException();
        }

    }

}