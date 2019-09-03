using System;
using coworkdomain.InventoryManagement;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class WareBookingBuilder : IModelBuilderSql<WareBooking> {

        public WareBooking CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var wareBooking = new WareBooking();
            var userBuilder = new UserBuilder();
            var wareBuilder = new WareBuilder();
            wareBooking.Id = dbHandler.GetValue<long>(0);
            wareBooking.WareId = dbHandler.GetValue<long>(1);
            wareBooking.UserId = dbHandler.GetValue<long>(2);
            wareBooking.Start = dbHandler.GetValue<DateTime>(3);
            wareBooking.End = dbHandler.GetValue<DateTime>(4);
            wareBooking.User = userBuilder.CreateDomainModel(dbHandler, 5 + startingIndex, out nextStartingIndex);
            wareBooking.Ware = wareBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            return wareBooking;
        }

    }

}