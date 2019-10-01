using System;

namespace cowork.usecases.TicketComment.Models {

    public class CreateTicketCommentInput {

        public CreateTicketCommentInput(string content, long ticketId, long authorId, DateTime created) {
            Content = content;
            TicketId = ticketId;
            AuthorId = authorId;
            Created = created;
        }
        
        
        public string Content { get; set; }
        public long TicketId { get; set; }
        public long AuthorId { get; set; }
        public DateTime Created { get; set; }

    }

}