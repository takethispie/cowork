using System;
using coworkdomain.Cowork;
using coworkpersistence.Handlers;

namespace coworkpersistence.DomainBuilders {

    public class SubscriptionBuilder : IModelBuilderSql<Subscription> {

        public Subscription CreateDomainModel(ISqlDbHandler dbHandler, int startingIndex, out int nextStartingIndex) {
            var subTypeBuilder = new SubscriptionTypeBuilder();
            var clientBuilder = new UserBuilder();
            var placeBuilder = new PlaceBuilder();
            var sub = new Subscription();
            sub.Id = dbHandler.GetValue<long>(0 + startingIndex);
            sub.TypeId = dbHandler.GetValue<long>(1 + startingIndex);
            sub.LatestRenewal = dbHandler.GetValue<DateTime>(2 + startingIndex);
            sub.ClientId = dbHandler.GetValue<long>(3 + startingIndex);
            sub.PlaceId = dbHandler.GetValue<long>(4 + startingIndex);
            sub.FixedContract = dbHandler.GetValue<bool>(5 + startingIndex);
            sub.Type = subTypeBuilder.CreateDomainModel(dbHandler, 6 + startingIndex, out nextStartingIndex);
            sub.Place = placeBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            sub.Client = clientBuilder.CreateDomainModel(dbHandler, nextStartingIndex, out nextStartingIndex);
            return sub;
        }

    }

}