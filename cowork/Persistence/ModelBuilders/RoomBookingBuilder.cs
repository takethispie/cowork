using System;
using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class RoomBookingBuilder : IModelBuilderSql<RoomBooking> {

        public RoomBooking CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var userBuilder = new UserBuilder();
            var roomBuilder = new RoomBuilder();
            var booking = new RoomBooking {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                RoomId = dbHandler.GetValue<long>(1 + startingIndex),
                ClientId = dbHandler.GetValue<long>(2 + startingIndex),
                Start = dbHandler.GetValue<DateTime>(3 + startingIndex),
                End = dbHandler.GetValue<DateTime>(4 + startingIndex)
            };
            booking.Client = userBuilder.CreateDomainModel(dbHandler, 5 + startingIndex, out nextStartingIndex);
            booking.Room = roomBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            return booking;
        }
    }

}