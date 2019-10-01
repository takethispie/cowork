using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetAllTickets : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;

        public GetAllTickets(IUserRepository userRepository, ITicketAttributionRepository ticketAttributionRepository, 
                             ITicketRepository ticketRepository, ITicketCommentRepository ticketCommentRepository) {
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketRepository = ticketRepository;
            this.ticketCommentRepository = ticketCommentRepository;
        }


        public IEnumerable<domain.Ticket> Execute() {
            return ticketRepository.GetAll().Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
        }

    }

}