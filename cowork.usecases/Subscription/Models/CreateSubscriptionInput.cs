using System;
using cowork.domain;

namespace cowork.usecases.Subscription.Models {

    public class CreateSubscriptionInput {

        public CreateSubscriptionInput(long typeId, long clientId, long placeId, DateTime latestRenewal, bool fixedContract, domain.SubscriptionType type) {
            TypeId = typeId;
            ClientId = clientId;
            PlaceId = placeId;
            LatestRenewal = latestRenewal;
            FixedContract = fixedContract;
            Type = type;
        }
        public long TypeId { get; set; }
        public long ClientId { get; set; }
        public long PlaceId { get; set; }
        public DateTime LatestRenewal { get; set; }
        public bool FixedContract { get; set; }
        public domain.SubscriptionType Type { get; set; }

    }

}