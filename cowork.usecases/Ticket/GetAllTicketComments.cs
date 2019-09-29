using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetAllTicketComments : IUseCase<IEnumerable<TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;

        public GetAllTicketComments(ITicketCommentRepository ticketCommentRepository) {
            this.ticketCommentRepository = ticketCommentRepository;
        }


        public IEnumerable<TicketComment> Execute() {
            return ticketCommentRepository.GetAll();
        }

    }

}