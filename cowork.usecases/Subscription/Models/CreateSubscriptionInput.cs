using System;
using cowork.domain;

namespace cowork.usecases.Subscription.Models {

    public class CreateSubscriptionInput {

        public long Id { get; set; }
        public long TypeId { get; set; }
        public long ClientId { get; set; }
        public long PlaceId { get; set; }
        public DateTime LatestRenewal { get; set; }
        public bool FixedContract { get; set; }
        public SubscriptionType Type { get; set; }

    }

}