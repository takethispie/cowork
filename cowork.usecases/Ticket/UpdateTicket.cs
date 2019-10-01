using cowork.domain.Interfaces;
using cowork.usecases.Ticket.Models;

namespace cowork.usecases.Ticket {

    public class UpdateTicket : IUseCase<long> {

        private readonly ITicketRepository ticketRepository;
        public readonly TicketInput Input;

        public UpdateTicket(ITicketRepository ticketRepository, TicketInput input) {
            this.ticketRepository = ticketRepository;
            Input = input;
        }


        public long Execute() {
            if (Input == null) return -1;
            var ticket = new domain.Ticket(Input.OpenedById, Input.State, Input.Description, Input.PlaceId, Input.Title,
                Input.Created);
            return ticketRepository.Update(ticket);
        }

    }

}