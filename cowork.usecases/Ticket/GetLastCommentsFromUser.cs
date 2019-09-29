using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetLastCommentsFromUser : IUseCase<IEnumerable<TicketComment>> {

        private readonly ITicketCommentRepository ticketCommentRepository;
        public readonly int Amount;
        public readonly long UserId;

        public GetLastCommentsFromUser(ITicketCommentRepository ticketCommentRepository, int amount, long userId) {
            this.ticketCommentRepository = ticketCommentRepository;
            Amount = amount;
            UserId = userId;
        }


        public IEnumerable<TicketComment> Execute() {
            return ticketCommentRepository.LastCommentsFromUser(UserId, Amount);
        }

    }

}