using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class TicketAttributionBuilder : IModelBuilderSql<TicketAttribution> {

        public TicketAttribution CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex,
                                                   out int nextStartingIndex) {
            var ticketAtt = new TicketAttribution();
            ticketAtt.Id = dbHandler.GetValue<long>(0);
            ticketAtt.StaffId = dbHandler.GetValue<long>(1);
            ticketAtt.TicketId = dbHandler.GetValue<long>(2);
            nextStartingIndex = 3 + startingIndex;
            return ticketAtt;
        }

    }

}