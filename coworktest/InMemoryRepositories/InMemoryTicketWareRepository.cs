using System.Collections.Generic;
using System.Linq;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketWareRepository : ITicketWareRepository {

        public List<TicketWare> TicketWares;


        public InMemoryTicketWareRepository() {
            TicketWares = new List<TicketWare>();
        }
        
        public long Create(TicketWare ticketWare) {
            var id = TicketWares.Count;
            ticketWare.Id = id;
            TicketWares.Add(ticketWare);
            return id;
        }


        public bool Delete(long id) {
            var item = TicketWares.Find(b => b.Id == id);
            if (item == null) return false;
            TicketWares.Remove(item);
            return true;
        }


        public long Update(TicketWare item) {
            long id = -1;
            TicketWares = TicketWares.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public List<TicketWare> GetAll() {
            return TicketWares;
        }


        public List<TicketWare> GetAllWithPaging(int page, int amount) {
            return TicketWares.Skip(page * amount).Take(amount).ToList();
        }


        public TicketWare GetById(long id) {
            return TicketWares.Find(t => t.Id == id);
        }


        public TicketWare GetByTicketId(long id) {
            return TicketWares.Find(t => t.TicketId == id);
        }

    }

}