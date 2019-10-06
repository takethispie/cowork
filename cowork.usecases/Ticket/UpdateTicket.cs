using cowork.domain.Interfaces;
using cowork.usecases.Ticket.Models;

namespace cowork.usecases.Ticket {

    public class UpdateTicket : IUseCase<long> {

        private readonly ITicketRepository ticketRepository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        public readonly UpdateTicketInput Input;
        private readonly long userId;


        public UpdateTicket(ITicketRepository ticketRepository, ITicketAttributionRepository ticketAttributionRepository,
                            UpdateTicketInput input, long userId) {
            this.ticketRepository = ticketRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            Input = input;
            this.userId = userId;
        }


        public long Execute() {
            if (Input == null) return -1;
            var attr = ticketAttributionRepository.GetFromTicket(Input.Id);
            if (attr?.StaffId != userId) return -1;
            var ticket = new domain.Ticket(Input.Id, Input.OpenedById, Input.State, Input.Description, Input.PlaceId, Input.Title,
                Input.Created);
            return ticketRepository.Update(ticket);
        }

    }

}