using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketById : IUseCase<domain.Ticket> {

        private readonly ITicketRepository ticketRepository;
        public readonly long Id;

        public GetTicketById(ITicketRepository ticketRepository, long id) {
            this.ticketRepository = ticketRepository;
            Id = id;
        }


        public domain.Ticket Execute() {
            return Id == -1 ? null : ticketRepository.GetById(Id);
        }

    }

}