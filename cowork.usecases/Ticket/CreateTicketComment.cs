using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class CreateTicketComment : IUseCase<long> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly TicketComment TicketComment;

        public CreateTicketComment(ITicketCommentRepository ticketCommentRepository, TicketComment ticketComment) {
            this.ticketCommentRepository = ticketCommentRepository;
            TicketComment = ticketComment;
        }


        public long Execute() {
            return ticketCommentRepository.Create(TicketComment);
        }

    }

}