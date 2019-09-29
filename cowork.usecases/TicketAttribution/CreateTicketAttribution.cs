using cowork.domain.Interfaces;

namespace cowork.usecases.TicketAttribution {

    public class CreateTicketAttribution : IUseCase<long> {

        private readonly ITicketAttributionRepository ticketAttributionRepository;
        public readonly domain.TicketAttribution TicketAttribution;

        public CreateTicketAttribution(ITicketAttributionRepository ticketAttributionRepository, domain.TicketAttribution ticketAttribution) {
            this.ticketAttributionRepository = ticketAttributionRepository;
            TicketAttribution = ticketAttribution;
        }


        public long Execute() {
            return ticketAttributionRepository.Create(TicketAttribution);
        }

    }

}