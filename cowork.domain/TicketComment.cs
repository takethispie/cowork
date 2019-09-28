using System;

namespace cowork.domain {

    public class TicketComment {

        public TicketComment() { }


        public TicketComment(long id, string content, long ticketId, long authorId, DateTime created) {
            Id = id;
            Content = content;
            TicketId = ticketId;
            AuthorId = authorId;
            Created = created;
        }


        public long Id { get; set; }
        public string Content { get; set; }
        public long TicketId { get; set; }
        public long AuthorId { get; set; }
        public DateTime Created { get; set; }
        public User Author { get; set; }

    }

}