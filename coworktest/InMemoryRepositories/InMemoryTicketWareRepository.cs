using System.Collections.Generic;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketWareRepository : ITicketWareRepository {

        public List<TicketWare> TicketWares;


        public InMemoryTicketWareRepository() {
            TicketWares = new List<TicketWare>();
        }
        
        public long Create(TicketWare ticketWare) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(TicketWare ticketWare) {
            throw new System.NotImplementedException();
        }


        public List<TicketWare> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<TicketWare> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public TicketWare GetById(long id) {
            throw new System.NotImplementedException();
        }


        public TicketWare GetByTicketId(long id) {
            throw new System.NotImplementedException();
        }

    }

}