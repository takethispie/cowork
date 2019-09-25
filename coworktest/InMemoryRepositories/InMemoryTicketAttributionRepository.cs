using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketAttributionRepository : ITicketAttributionRepository {

        public List<TicketAttribution> TicketAttributions;


        public InMemoryTicketAttributionRepository() {
            TicketAttributions = new List<TicketAttribution>();
        }
        
        public List<TicketAttribution> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<TicketAttribution> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public TicketAttribution GetById(long id) {
            throw new System.NotImplementedException();
        }


        public List<TicketAttribution> GetAllFromStaffId(long id) {
            throw new System.NotImplementedException();
        }


        public TicketAttribution GetFromTicket(long ticketId) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Create(TicketAttribution ticketAttribution) {
            throw new System.NotImplementedException();
        }


        public long Update(TicketAttribution ticketAttribution) {
            throw new System.NotImplementedException();
        }

    }

}