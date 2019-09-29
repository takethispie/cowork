using System;

namespace cowork.usecases.Ticket.Models {

    public class CreateTicketInput {

        public string Content { get; set; }
        public long TicketId { get; set; }
        public long AuthorId { get; set; }
        public DateTime Created { get; set; }

    }

}