using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketWare {

    public class GetTicketWaresWithPaging : IUseCase<IEnumerable<domain.TicketWare>> {

        private readonly ITicketWareRepository ticketWareRepository;
        public readonly int Amount;
        public readonly int Page;

        public GetTicketWaresWithPaging(ITicketWareRepository ticketWareRepository, int amount, int page) {
            this.ticketWareRepository = ticketWareRepository;
            Amount = amount;
            Page = page;
        }


        public IEnumerable<domain.TicketWare> Execute() {
            return ticketWareRepository.GetAllWithPaging(Page, Amount);
        }

    }

}