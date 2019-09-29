using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketAttribution {

    public class GetAllTicketAttributions : IUseCase<IEnumerable<domain.TicketAttribution>> {

        private readonly ITicketAttributionRepository ticketAttributionRepository;

        public GetAllTicketAttributions(ITicketAttributionRepository ticketAttributionRepository) {
            this.ticketAttributionRepository = ticketAttributionRepository;
        }


        public IEnumerable<domain.TicketAttribution> Execute() {
            return ticketAttributionRepository.GetAll();
        }

    }

}