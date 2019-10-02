using System;
using cowork.domain;

namespace cowork.usecases.Ticket.Models {

    public class UpdateTicketInput {

        public UpdateTicketInput(long id, long openedById, TicketState state, string description, long placeId, string title, DateTime created) {
            Id = id;
            OpenedById = openedById;
            State = state;
            Description = description;
            PlaceId = placeId;
            Title = title;
            Created = created;
        }

        public long Id { get; set; }
        public long OpenedById { get; set; }
        public TicketState State { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }

    }

}