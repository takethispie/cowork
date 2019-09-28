using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class TicketWareBuilder : IModelBuilderSql<TicketWare> {

        public TicketWare CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex,
                                            out int nextStartingIndex) {
            var wareBuilder = new WareBuilder();
            var ticketWare = new TicketWare();
            ticketWare.Id = dbHandler.GetValue<long>(0 + startingIndex);
            ticketWare.TicketId = dbHandler.GetValue<long>(1 + startingIndex);
            ticketWare.WareId = dbHandler.GetValue<long>(2 + startingIndex);
            ticketWare.Ware = wareBuilder.CreateDomainModel(dbHandler, 3 + startingIndex, out nextStartingIndex);
            return ticketWare;
        }

    }

}