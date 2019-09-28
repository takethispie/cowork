using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface ITicketCommentRepository {

        long Create(TicketComment ticketComment);

        bool Delete(long id);

        long Update(TicketComment ticketComment);

        List<TicketComment> GetAll();
        List<TicketComment> GetAllWithPaging(int page, int amount);

        TicketComment GetById(long id);

        List<TicketComment> GetByTicketId(long ticketId);
        List<TicketComment> LastCommentsFromUser(long userId, int numberOfComments);

    }

}