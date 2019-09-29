using System;
using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class GetSubscriptionById : IUseCase<domain.Subscription> {

        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long Id;

        public GetSubscriptionById(ISubscriptionRepository subscriptionRepository, ITimeSlotRepository timeSlotRepository, long id) {
            this.subscriptionRepository = subscriptionRepository;
            this.timeSlotRepository = timeSlotRepository;
            Id = id;
        }


        public domain.Subscription Execute() {
            var sub = subscriptionRepository.GetById(Id);
            if(sub == null) throw new ArgumentNullException("aucun abonnement ne correspond à l'id");
            sub.Place.OpenedTimes = timeSlotRepository.GetAllOfPlace(sub.PlaceId);
            return sub;
        }

    }

}