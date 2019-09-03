using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class SubscriptionTypeBuilder : IModelBuilderSql<SubscriptionType> {

        public SubscriptionType CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var subType = new SubscriptionType {
                Id = dbHandler.GetValue<long>(0 + startingIndex),
                Name = dbHandler.GetValue<string>(1 + startingIndex),
                FixedContractDurationMonth = dbHandler.GetValue<long>(2 + startingIndex),
                PriceFirstHour = dbHandler.GetValue<double>(3 + startingIndex),
                PriceNextHalfHour = dbHandler.GetValue<double>(4 + startingIndex),
                PriceDay = dbHandler.GetValue<double>(5 + startingIndex),
                PriceDayStudent = dbHandler.GetValue<double>(6 + startingIndex),
                MonthlyFeeFixedContract = dbHandler.GetValue<double>(7 + startingIndex),
                MonthlyFeeContractFree = dbHandler.GetValue<double>(8 + startingIndex),
                Description =  dbHandler.GetValue<string>(9 + startingIndex)
            };
            nextStartingIndex = 10 + startingIndex;
            return subType;
        }
    }

}