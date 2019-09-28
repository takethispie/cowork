using cowork.domain;
using cowork.persistence.Handlers;

namespace cowork.persistence.ModelBuilders {

    public class WareBuilder : IModelBuilderSql<Ware> {

        public Ware CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var placeBuilder = new PlaceBuilder();
            var ware = new Ware {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                Name = dbHandler.GetValue<string>(1 + startingIndex),
                Description = dbHandler.GetValue<string>(2 + startingIndex),
                SerialNumber = dbHandler.GetValue<string>(3 + startingIndex),
                PlaceId = dbHandler.GetValue<long>(4 + startingIndex),
                InStorage = dbHandler.GetValue<bool>(5 + startingIndex)
            };
            ware.Place = placeBuilder.CreateDomainModel(dbHandler, 6 + startingIndex, out nextStartingIndex);
            return ware;
        }

    }

}