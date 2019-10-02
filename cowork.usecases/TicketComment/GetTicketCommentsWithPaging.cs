using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketComment {

    public class GetTicketCommentsWithPaging : IUseCase<IEnumerable<domain.TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetTicketCommentsWithPaging(ITicketCommentRepository ticketCommentRepository, int page, int amount) {
            this.ticketCommentRepository = ticketCommentRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.TicketComment> Execute() {
            return ticketCommentRepository.GetAllWithPaging(Page, Amount);
        }

    }

}