using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsOfPlace : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long PlaceId;

        public GetTicketsOfPlace(IUserRepository userRepository, ITicketAttributionRepository ticketAttributionRepository, ITicketRepository ticketRepository, ITicketCommentRepository ticketCommentRepository, long placeId) {
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketRepository = ticketRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.Ticket> Execute() {
            return ticketRepository.GetAllOfPlace(PlaceId).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
        }

    }

}