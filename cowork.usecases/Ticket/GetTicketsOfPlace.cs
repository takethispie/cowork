using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ticket {

    public class GetTicketsOfPlace : IUseCase<IEnumerable<domain.Ticket>> {

        private readonly ITicketRepository ticketRepository;
        public readonly long PlaceId;

        public GetTicketsOfPlace(ITicketRepository ticketRepository, long placeId) {
            this.ticketRepository = ticketRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.Ticket> Execute() {
            return ticketRepository.GetAllOfPlace(PlaceId);
        }

    }

}