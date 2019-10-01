using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketById : IUseCase<domain.Ticket> {

        private readonly ITicketRepository ticketRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly IUserRepository userRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long Id;

        public GetTicketById(ITicketRepository ticketRepository, 
                             ITicketAttributionRepository ticketAttributionRepository, 
                             IUserRepository userRepository, 
                             ITicketCommentRepository ticketCommentRepository, long id) {
            this.ticketRepository = ticketRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.userRepository = userRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            Id = id;
        }


        public domain.Ticket Execute() {
            var result = ticketRepository.GetById(Id);
            if (result == null) return null;
            var ticketAttribution = ticketAttributionRepository.GetFromTicket(result.Id);
            if (ticketAttribution != null)
                result.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
            result.Comments = ticketCommentRepository.GetByTicketId(result.Id);
            return result;
        }

    }

}