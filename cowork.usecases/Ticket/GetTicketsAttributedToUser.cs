using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsAttributedToUser : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly IUserRepository userRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly long UserId;

        public GetTicketsAttributedToUser(IUserRepository userRepository, ITicketAttributionRepository ticketAttributionRepository, ITicketRepository ticketRepository, ITicketCommentRepository ticketCommentRepository, long userId) {
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketRepository = ticketRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            UserId = userId;
        }


        public IEnumerable<domain.Ticket> Execute() {
            var personnal = userRepository.GetById(UserId);
            if (personnal == null) throw new Exception("Impossible de trouver l'utilisateur'");
            return ticketAttributionRepository.GetAllFromStaffId(UserId)
                .Select(ticketAttr => {
                    var ticket = ticketRepository.GetById(ticketAttr.TicketId);
                    ticket.AttributedTo = userRepository.GetById(ticketAttr.StaffId);
                    return ticket;
                })
                .Select(ticket => {
                    ticket.Comments =
                        ticketCommentRepository.GetByTicketId(ticket.Id);
                    return ticket;
                });
        }



    }

}