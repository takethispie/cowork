using System;
using System.Collections.Generic;

namespace cowork.domain {

    public class Ticket {

        public Ticket() {
            Comments = new List<TicketComment>();
        }


        public Ticket(long id, long openedById, TicketState state, string description, long placeId, string title,
                      DateTime created) {
            Id = id;
            OpenedById = openedById;
            State = state;
            Description = description;
            PlaceId = placeId;
            Title = title;
            Created = created;
            Comments = new List<TicketComment>();
        }


        public long Id { get; set; }
        public long OpenedById { get; set; }
        public TicketState State { get; set; }
        public string Description { get; set; }
        public long PlaceId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public User OpenedBy { get; set; }
        public Ware Ware { get; set; }
        public User AttributedTo { get; set; }
        public Place Place { get; set; }
        public List<TicketComment> Comments { get; set; }

    }

}