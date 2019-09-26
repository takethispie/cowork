using System.Collections.Generic;
using System.Linq;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketAttributionRepository : ITicketAttributionRepository {

        public List<TicketAttribution> TicketAttributions;


        public InMemoryTicketAttributionRepository() {
            TicketAttributions = new List<TicketAttribution>();
        }
        
        public List<TicketAttribution> GetAll() {
            return TicketAttributions;
        }


        public List<TicketAttribution> GetAllWithPaging(int page, int amount) {
            return TicketAttributions.Skip(page * amount).Take(amount).ToList();
        }


        public TicketAttribution GetById(long id) {
            return TicketAttributions.Find(t => t.Id == id);
        }


        public List<TicketAttribution> GetAllFromStaffId(long id) {
            return TicketAttributions.FindAll(t => t.StaffId == id);
        }


        public TicketAttribution GetFromTicket(long ticketId) {
            return TicketAttributions.Find(t => t.TicketId == ticketId);
        }


        public bool Delete(long id) {
            var item = TicketAttributions.Find(b => b.Id == id);
            if (item == null) return false;
            TicketAttributions.Remove(item);
            return true;
        }


        public long Create(TicketAttribution ticketAttribution) {
            var id = TicketAttributions.Count;
            ticketAttribution.Id = id;
            TicketAttributions.Add(ticketAttribution);
            return id;
        }


        public long Update(TicketAttribution item) {
            long id = -1;
            TicketAttributions = TicketAttributions.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }

    }

}