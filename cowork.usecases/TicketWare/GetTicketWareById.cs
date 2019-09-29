using cowork.domain.Interfaces;

namespace cowork.usecases.TicketWare {

    public class GetTicketWareById : IUseCase<domain.TicketWare> {

        private readonly ITicketWareRepository ticketWareRepository;
        public readonly long Id;

        public GetTicketWareById(ITicketWareRepository ticketWareRepository, long id) {
            this.ticketWareRepository = ticketWareRepository;
            Id = id;
        }


        public domain.TicketWare Execute() {
            return ticketWareRepository.GetById(Id);
        }

    }

}