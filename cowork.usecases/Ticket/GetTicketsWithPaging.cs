using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsWithPaging : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly ITicketRepository ticketRepository;
        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int Page;
        public readonly int Amount;


        public GetTicketsWithPaging(ITicketRepository ticketRepository, IUserRepository userRepository,
                                      ITicketAttributionRepository ticketAttributionRepository, 
                                      ITicketCommentRepository ticketCommentRepository, int page, int amount) {
            this.ticketRepository = ticketRepository;
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Ticket> Execute() {
            var result = ticketRepository.GetAllByPaging(Page, Amount).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return result;
        }

    }

}