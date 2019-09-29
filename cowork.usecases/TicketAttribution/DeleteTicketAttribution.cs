using cowork.domain.Interfaces;

namespace cowork.usecases.TicketAttribution {

    public class DeleteTicketAttribution : IUseCase<bool> {

        private readonly ITicketAttributionRepository ticketAttributionRepository;
        public readonly long Id;

        public DeleteTicketAttribution(ITicketAttributionRepository ticketAttributionRepository, long id) {
            this.ticketAttributionRepository = ticketAttributionRepository;
            Id = id;
        }


        public bool Execute() {
            return ticketAttributionRepository.Delete(Id);
        }

    }

}