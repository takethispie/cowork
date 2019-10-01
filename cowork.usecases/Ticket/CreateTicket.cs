using cowork.domain.Interfaces;
using cowork.usecases.Ticket.Models;

namespace cowork.usecases.Ticket {

    public class CreateTicket : IUseCase<long> {

        private readonly ITicketRepository ticketRepository;
        public readonly CreateTicketInput Input;

        public CreateTicket(ITicketRepository ticketRepository, CreateTicketInput input) {
            this.ticketRepository = ticketRepository;
            Input = input;
        }


        public long Execute() {
            if (Input == null) return -1;
            var ticket = new domain.Ticket(Input.OpenedById, Input.State, Input.Description, Input.PlaceId, Input.Title,
                Input.Created);
            return ticketRepository.Create(ticket);
        }

    }

}