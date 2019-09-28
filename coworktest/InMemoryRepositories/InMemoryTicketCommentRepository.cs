using System.Collections.Generic;
using System.Linq;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketCommentRepository : ITicketCommentRepository {

        public List<TicketComment> TicketComments;


        public InMemoryTicketCommentRepository() {
            TicketComments = new List<TicketComment>();
        }
        
        public long Create(TicketComment ticketComment) {
            var id = TicketComments.Count;
            ticketComment.Id = id;
            TicketComments.Add(ticketComment);
            return id;
        }


        public bool Delete(long id) {
            var item = TicketComments.Find(b => b.Id == id);
            if (item == null) return false;
            TicketComments.Remove(item);
            return true;
        }


        public long Update(TicketComment item) {
            long id = -1;
            TicketComments = TicketComments.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public List<TicketComment> GetAll() {
            return TicketComments;
        }


        public List<TicketComment> GetAllWithPaging(int page, int amount) {
            return TicketComments.Skip(page * amount).Take(amount).ToList();
        }


        public TicketComment GetById(long id) {
            return TicketComments.Find(t => t.Id == id);
        }


        public List<TicketComment> GetByTicketId(long ticketId) {
            return TicketComments.FindAll(t => t.TicketId == ticketId);
        }


        public List<TicketComment> LastCommentsFromUser(long userId, int numberOfComments) {
            return TicketComments.Where(t => t.AuthorId == userId).OrderByDescending(t => t.Created)
                .Take(numberOfComments).ToList();
        }

    }

}