using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface ITicketCommentRepository {

        long Create(TicketComment ticketComment);

        bool Delete(long id);

        long Update(TicketComment ticketComment);

        List<TicketComment> GetAll();

        TicketComment GetById(long id);

        List<TicketComment> GetByTicketId(long ticketId);

    }

}