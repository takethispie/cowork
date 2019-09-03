using coworkdomain.InventoryManagement;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class StaffLocationBuilder : IModelBuilderSql<StaffLocation> {

        public StaffLocation CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var staffLocation = new StaffLocation {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                UserId = dbHandler.GetValue<long>(1 + startingIndex),
                PlaceId = dbHandler.GetValue<long>(2 + startingIndex)
            };
            nextStartingIndex = 3 + startingIndex;
            return staffLocation;
        }

    }

}