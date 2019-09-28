using System;

namespace cowork.domain {

    public class Subscription {

        public Subscription(long id, long typeId, long clientId, DateTime latestRenewal, long placeId,
                            bool fixedContract) {
            Id = id;
            TypeId = typeId;
            ClientId = clientId;
            LatestRenewal = latestRenewal;
            PlaceId = placeId;
            FixedContract = fixedContract;
        }


        public Subscription() { }

        public long Id { get; set; }
        public long TypeId { get; set; }
        public long ClientId { get; set; }
        public long PlaceId { get; set; }
        public DateTime LatestRenewal { get; set; }
        public bool FixedContract { get; set; }
        public SubscriptionType Type { get; set; }
        public Place Place { get; set; }
        public User Client { get; set; }

    }

}