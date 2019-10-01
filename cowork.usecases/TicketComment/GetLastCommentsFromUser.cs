using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketComment {

    public class GetLastCommentsFromUser : IUseCase<IEnumerable<domain.TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int Amount;
        public readonly long UserId;

        public GetLastCommentsFromUser(ITicketCommentRepository ticketCommentRepository, int amount, long userId) {
            this.ticketCommentRepository = ticketCommentRepository;
            Amount = amount;
            UserId = userId;
        }


        public IEnumerable<domain.TicketComment> Execute() {
            return ticketCommentRepository.LastCommentsFromUser(UserId, Amount);
        }

    }

}