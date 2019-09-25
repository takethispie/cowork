using System.Collections.Generic;
using coworkdomain.Cowork;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTicketRepository : ITicketRepository {

        public List<Ticket> Tickets;


        public InMemoryTicketRepository() {
            Tickets = new List<Ticket>();
        }
        
        public List<Ticket> GetAll() {
            throw new System.NotImplementedException();
        }


        public List<Ticket> GetAllOfPlace(long placeId) {
            throw new System.NotImplementedException();
        }


        public Ticket GetById(long id) {
            throw new System.NotImplementedException();
        }


        public List<Ticket> GetAllOpenedBy(User user) {
            throw new System.NotImplementedException();
        }


        public List<Ticket> GetAllByPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public List<Ticket> GetAllWithState(int state) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(Ticket ticket) {
            throw new System.NotImplementedException();
        }


        public long Create(Ticket ticket) {
            throw new System.NotImplementedException();
        }

    }

}