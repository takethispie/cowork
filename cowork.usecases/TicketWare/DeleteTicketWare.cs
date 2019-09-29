using cowork.domain.Interfaces;

namespace cowork.usecases.TicketWare {

    public class DeleteTicketWare : IUseCase<bool> {

        private readonly ITicketWareRepository ticketWareRepository;
        public readonly long Id;

        public DeleteTicketWare(ITicketWareRepository ticketWareRepository, long id) {
            this.ticketWareRepository = ticketWareRepository;
            Id = id;
        }


        public bool Execute() {
            return ticketWareRepository.Delete(Id);
        }

    }

}