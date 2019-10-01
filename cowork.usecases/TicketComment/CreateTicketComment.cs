using cowork.domain.Interfaces;

namespace cowork.usecases.TicketComment {

    public class CreateTicketComment : IUseCase<long> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly domain.TicketComment TicketComment;

        public CreateTicketComment(ITicketCommentRepository ticketCommentRepository, domain.TicketComment ticketComment) {
            this.ticketCommentRepository = ticketCommentRepository;
            TicketComment = ticketComment;
        }


        public long Execute() {
            return ticketCommentRepository.Create(TicketComment);
        }

    }

}