using System;
using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class TimeSlotBuilder : IModelBuilderSql<TimeSlot> {

        public TimeSlot CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var placeBuilder = new PlaceBuilder();
            var successParsing =
                Enum.TryParse<DayOfWeek>(dbHandler.GetValue<long>(1 + startingIndex).ToString(), out var day);
            if (!successParsing) throw new Exception("Error parsing RoomType");
            var timeslot = new TimeSlot {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                StartHour = dbHandler.GetValue<short>(2 + startingIndex),
                StartMinutes = dbHandler.GetValue<short>(3 + startingIndex),
                EndHour = dbHandler.GetValue<short>(4 + startingIndex),
                EndMinutes = dbHandler.GetValue<short>(5 + startingIndex),
                PlaceId = dbHandler.GetValue<long>(6 + startingIndex),
                Day = day
            };
            timeslot.Place = placeBuilder.CreateDomainModel(dbHandler, 7 + startingIndex, out nextStartingIndex);
            return timeslot;
        }

    }

}