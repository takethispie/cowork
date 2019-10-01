using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketComment {

    public class GetAllTicketComments : IUseCase<IEnumerable<domain.TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;

        public GetAllTicketComments(ITicketCommentRepository ticketCommentRepository) {
            this.ticketCommentRepository = ticketCommentRepository;
        }


        public IEnumerable<domain.TicketComment> Execute() {
            return ticketCommentRepository.GetAll();
        }

    }

}