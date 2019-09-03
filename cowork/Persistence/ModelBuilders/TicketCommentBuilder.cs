using System;
using coworkdomain.InventoryManagement;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class TicketCommentBuilder : IModelBuilderSql<TicketComment> {

        public TicketComment CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var userBuilder = new UserBuilder();
            var ticketComment = new TicketComment();
            ticketComment.Id = dbHandler.GetValue<long>(0 + startingIndex);
            ticketComment.Content = dbHandler.GetValue<string>(1 + startingIndex);
            ticketComment.TicketId = dbHandler.GetValue<long>(2 + startingIndex);
            ticketComment.AuthorId = dbHandler.GetValue<long>(3 + startingIndex);
            ticketComment.Created = dbHandler.GetValue<DateTime>(4 + startingIndex);
            ticketComment.Author = userBuilder.CreateDomainModel(dbHandler, 5 + startingIndex, out nextStartingIndex);
            return ticketComment;
        }

    }

}