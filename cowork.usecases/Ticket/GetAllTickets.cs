using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetAllTickets : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly ITicketRepository ticketRepository;

        public IEnumerable<domain.Ticket> Execute() {
            return ticketRepository.GetAll();
        }

    }

}