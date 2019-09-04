using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface ITicketWareRepository {

        long Create(TicketWare ticketWare);

        bool Delete(long id);

        long Update(TicketWare ticketWare);

        List<TicketWare> GetAll();
        List<TicketWare> GetAllWithPaging(int page, int amount);

        TicketWare GetById(long id);

        TicketWare GetByTicketId(long id);

    }

}