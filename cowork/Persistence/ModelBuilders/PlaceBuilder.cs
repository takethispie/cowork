using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class PlaceBuilder : IModelBuilderSql<Place> {

        public Place CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var result = new Place();
            result.Id = dbHandler.GetValue<long>(0 + startingIndex);
            result.Name = dbHandler.GetValue<string>(1 + startingIndex);
            result.HighBandwidthWifi = dbHandler.GetValue<bool>(2 + startingIndex);
            result.MembersOnlyArea = dbHandler.GetValue<bool>(3 + startingIndex);
            result.UnlimitedBeverages = dbHandler.GetValue<bool>(4 + startingIndex);
            result.CosyRoomAmount = dbHandler.GetValue<int>(5 + startingIndex);
            result.LaptopAmount = dbHandler.GetValue<int>(6 + startingIndex);
            result.PrinterAmount = dbHandler.GetValue<int>(7 + startingIndex);
            nextStartingIndex = 8 + startingIndex;
            return result;
        }
    }

}