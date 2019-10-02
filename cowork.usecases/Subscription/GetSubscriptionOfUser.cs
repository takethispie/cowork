using System;
using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class GetSubscriptionOfUser : IUseCase<domain.Subscription> {

        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long UserId;

        public GetSubscriptionOfUser(ISubscriptionRepository subscriptionRepository, 
                                     ITimeSlotRepository timeSlotRepository, long userId) {
            this.subscriptionRepository = subscriptionRepository;
            UserId = userId;
            this.timeSlotRepository = timeSlotRepository;
        }


        public domain.Subscription Execute() {
            var res = subscriptionRepository.GetOfUser(UserId);
            if (res == null) throw new Exception("aucun abonnement correspondant à l'utiliseur");
            res.Place.OpenedTimes = timeSlotRepository.GetAllOfPlace(res.Place.Id);
            if (!res.FixedContract || res.LatestRenewal.AddMonths(res.Type.FixedContractDurationMonth) >= DateTime.Today) 
                return res;
            subscriptionRepository.Delete(res.Id);
            return null;

        }

    }

}