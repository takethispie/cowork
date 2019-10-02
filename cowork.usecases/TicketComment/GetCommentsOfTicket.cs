using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketComment {

    public class GetCommentsOfTicket : IUseCase<IEnumerable<domain.TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long TicketId;

        public GetCommentsOfTicket(ITicketCommentRepository ticketCommentRepository, long ticketId) {
            this.ticketCommentRepository = ticketCommentRepository;
            TicketId = ticketId;
        }


        public IEnumerable<domain.TicketComment> Execute() {
            return ticketCommentRepository.GetByTicketId(TicketId);
        }

    }

}