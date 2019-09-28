using System;
using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class RoomBuilder : IModelBuilderSql<Room> {

        public Room CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var placeBuilder = new PlaceBuilder();
            var successParsing =
                Enum.TryParse<RoomType>(dbHandler.GetValue<long>(3 + startingIndex).ToString(), out var type);
            if (!successParsing) throw new Exception("Error parsing RoomType");
            var room = new Room {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                Name = dbHandler.GetValue<string>(1 + startingIndex),
                PlaceId = dbHandler.GetValue<long>(2 + startingIndex),
                Type = type
            };
            room.Place = placeBuilder.CreateDomainModel(dbHandler, 4 + startingIndex, out nextStartingIndex);
            return room;
        }

    }

}