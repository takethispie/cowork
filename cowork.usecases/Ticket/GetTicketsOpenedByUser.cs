using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsOpenedByUser : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly ITicketRepository ticketRepository;
        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long UserId;

        public GetTicketsOpenedByUser(ITicketRepository ticketRepository, IUserRepository userRepository,
                                      ITicketAttributionRepository ticketAttributionRepository, 
                                      ITicketCommentRepository ticketCommentRepository, long userId) {
            this.ticketRepository = ticketRepository;
            UserId = userId;
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketCommentRepository = ticketCommentRepository;
        }


        public IEnumerable<domain.Ticket> Execute() {
            var user = userRepository.GetById(UserId);
            if (user == null) throw new Exception("l'utilisateur n'existe pas");
            var res = ticketRepository.GetAllOpenedBy(UserId).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return res;
        }

    }

}