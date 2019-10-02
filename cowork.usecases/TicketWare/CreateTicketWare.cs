using cowork.domain.Interfaces;
using cowork.usecases.TicketWare.Models;

namespace cowork.usecases.TicketWare {

    public class CreateTicketWare : IUseCase<long> {

        private readonly ITicketWareRepository ticketWareRepository;
        public readonly CreateTicketWareInput Input;

        public CreateTicketWare(ITicketWareRepository ticketWareRepository, CreateTicketWareInput input) {
            this.ticketWareRepository = ticketWareRepository;
            Input = input;
        }


        public long Execute() {
            var tw = new domain.TicketWare(Input.TicketId, Input.WareId);
            return ticketWareRepository.Create(tw);
        }

    }

}