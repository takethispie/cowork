using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetCommentsOfTicket : IUseCase<IEnumerable<TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long TicketId;

        public GetCommentsOfTicket(ITicketCommentRepository ticketCommentRepository, long ticketId) {
            this.ticketCommentRepository = ticketCommentRepository;
            TicketId = ticketId;
        }


        public IEnumerable<TicketComment> Execute() {
            return ticketCommentRepository.GetByTicketId(TicketId);
        }

    }

}