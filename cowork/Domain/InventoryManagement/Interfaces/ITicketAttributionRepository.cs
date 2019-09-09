using System.Collections.Generic;

namespace coworkdomain.InventoryManagement.Interfaces {

    public interface ITicketAttributionRepository {

        List<TicketAttribution> GetAll();
        List<TicketAttribution> GetAllWithPaging(int page, int amount);

        TicketAttribution GetById(long id);

        List<TicketAttribution> GetAllFromStaffId(long id);

        TicketAttribution GetFromTicket(long ticketId);

        bool Delete(long id);

        long Create(TicketAttribution ticketAttribution);

        long Update(TicketAttribution ticketAttribution);

    }

}