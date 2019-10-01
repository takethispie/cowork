using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class DeleteTicket : IUseCase<bool> {

        private readonly ITicketRepository ticketRepository;
        public readonly long Id;

        public DeleteTicket(ITicketRepository ticketRepository, long id) {
            this.ticketRepository = ticketRepository;
            Id = id;
        }


        public bool Execute() {
            return Id != -1 && ticketRepository.Delete(Id);
        }

    }

}