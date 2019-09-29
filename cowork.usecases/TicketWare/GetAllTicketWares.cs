using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TicketWare {

    public class GetAllTicketWares : IUseCase<IEnumerable<domain.TicketWare>> {

        private readonly ITicketWareRepository ticketWareRepository;

        public GetAllTicketWares(ITicketWareRepository ticketWareRepository) {
            this.ticketWareRepository = ticketWareRepository;
        }


        public IEnumerable<domain.TicketWare> Execute() {
            return ticketWareRepository.GetAll();
        }

    }

}