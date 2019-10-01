using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class DeleteTicket : IUseCase<bool> {

        private readonly ITicketRepository ticketRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketWareRepository ticketWareRepository;
        public readonly long Id;

        public DeleteTicket(ITicketRepository ticketRepository, 
                            ITicketCommentRepository ticketCommentRepository, 
                            ITicketAttributionRepository ticketAttributionRepository, 
                            ITicketWareRepository ticketWareRepository, long id) {
            this.ticketRepository = ticketRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            Id = id;
            this.ticketWareRepository = ticketWareRepository;
        }


        public bool Execute() {
            var comments = ticketCommentRepository.GetByTicketId(Id);
            comments.ForEach(comment => ticketCommentRepository.Delete(comment.Id));
            var attr = ticketAttributionRepository.GetFromTicket(Id);
            if(attr != null) ticketAttributionRepository.Delete(attr.Id);
            var ware = ticketWareRepository.GetByTicketId(Id);
            if (ware != null) ticketWareRepository.Delete(Id);
            return ticketRepository.Delete(Id);
        }

    }

}