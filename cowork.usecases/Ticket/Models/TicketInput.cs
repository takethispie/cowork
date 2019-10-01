using System;
using cowork.domain;

namespace cowork.usecases.Ticket.Models {

    public class TicketInput {

        public TicketInput(long openedById, TicketState state, string description, long placeId, string title, DateTime created) {
            OpenedById = openedById;
            State = state;
            Description = description;
            PlaceId = placeId;
            Title = title;
            Created = created;
        }
        
        public long OpenedById { get; set; }
        public TicketState State { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }

    }

}