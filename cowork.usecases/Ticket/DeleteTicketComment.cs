using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class DeleteTicketComment : IUseCase<bool> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long Id;

        public DeleteTicketComment(ITicketCommentRepository ticketCommentRepository, long id) {
            this.ticketCommentRepository = ticketCommentRepository;
            Id = id;
        }


        public bool Execute() {
            return ticketCommentRepository.Delete(Id);
        }

    }

}