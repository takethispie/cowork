using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketAttribution {

    public class GetTicketAttributionsWithPaging : IUseCase<IEnumerable<domain.TicketAttribution>> {

        private readonly ITicketAttributionRepository ticketAttributionRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetTicketAttributionsWithPaging(ITicketAttributionRepository ticketAttributionRepository, int page, int amount) {
            this.ticketAttributionRepository = ticketAttributionRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.TicketAttribution> Execute() {
            return ticketAttributionRepository.GetAllWithPaging(Page, Amount);
        }

    }

}