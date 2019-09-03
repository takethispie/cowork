using System.Collections.Generic;
using coworkdomain.Cowork;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface ITicketRepository {

        List<Ticket> GetAll();

        List<Ticket> GetAllOfPlace(long placeId);

        Ticket GetById(long id);

        List<Ticket> GetAllOpenedBy(User user);
        
        bool Delete(long id);

        long Update(Ticket ticket);

        long Create(Ticket ticket);

    }

}