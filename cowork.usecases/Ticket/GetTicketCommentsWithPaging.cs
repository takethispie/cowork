using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketCommentsWithPaging : IUseCase<IEnumerable<TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetTicketCommentsWithPaging(ITicketCommentRepository ticketCommentRepository, int page, int amount) {
            this.ticketCommentRepository = ticketCommentRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<TicketComment> Execute() {
            return ticketCommentRepository.GetAllWithPaging(Page, Amount);
        }

    }

}