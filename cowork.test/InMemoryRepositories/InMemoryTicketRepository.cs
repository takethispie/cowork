using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemoryTicketRepository : ITicketRepository {

        public List<Ticket> Tickets;


        public InMemoryTicketRepository() {
            Tickets = new List<Ticket>();
        }
        
        public List<Ticket> GetAll() {
            return Tickets;
        }


        public List<Ticket> GetAllOfPlace(long placeId) {
            return Tickets.FindAll(t => t.PlaceId == placeId);
        }


        public Ticket GetById(long id) {
            return Tickets.Find(t => t.Id == id);
        }


        public List<Ticket> GetAllOpenedBy(long userId) {
            return Tickets.FindAll(t => t.OpenedById == userId);
        }


        public List<Ticket> GetAllByPaging(int page, int amount) {
            return Tickets.Skip(page * amount).Take(amount).ToList();
        }


        public List<Ticket> GetAllWithState(int state) {
            return Tickets.FindAll(t => (int)t.State == state);
        }


        public bool Delete(long id) {
            var item = Tickets.Find(b => b.Id == id);
            if (item == null) return false;
            Tickets.Remove(item);
            return true;
        }


        public long Update(Ticket item) {
            long id = -1;
            Tickets = Tickets.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public long Create(Ticket ticket) {
            var id = Tickets.Count;
            ticket.Id = id;
            Tickets.Add(ticket);
            return id;
        }

    }

}