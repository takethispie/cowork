using System;
using coworkdomain.InventoryManagement;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class TicketBuilder : IModelBuilderSql<Ticket> {

        public Ticket CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var placeBuilder = new PlaceBuilder();
            var userBuilder = new UserBuilder();
            var successParsing = Enum.TryParse<TicketState>(dbHandler.GetValue<long>(2 + startingIndex).ToString(), out var state);
            if (!successParsing) throw new Exception("Error parsing TicketState");
            var ticket = new Ticket();
            ticket.Id = dbHandler.GetValue<long>(0 + startingIndex);
            ticket.OpenedById = dbHandler.GetValue<long>(1 + startingIndex);
            ticket.State = state;
            ticket.Description = dbHandler.GetValue<string>(3 + startingIndex);
            ticket.PlaceId = dbHandler.GetValue<long>(4 + startingIndex);
            ticket.Title = dbHandler.GetValue<string>(5 + startingIndex);
            ticket.Created = dbHandler.GetValue<DateTime>(6 + startingIndex);
            ticket.OpenedBy = userBuilder.CreateDomainModel(dbHandler, 7 + startingIndex, out nextStartingIndex);
            ticket.Place = placeBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            return ticket;
        }
    }

}