using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsWithState {

        private readonly ITicketRepository ticketRepository;
        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int State;


        public GetTicketsWithState(ITicketRepository ticketRepository, IUserRepository userRepository,
                                    ITicketAttributionRepository ticketAttributionRepository, 
                                    ITicketCommentRepository ticketCommentRepository, int state) {
            this.ticketRepository = ticketRepository;
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            State = state;
        }


        public IEnumerable<domain.Ticket> Execute() {
            return ticketRepository.GetAllWithState(State).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
        }

    }

}