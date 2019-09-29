using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class GetAllSubscriptions : IUseCase<IEnumerable<domain.Subscription>> {

        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ITimeSlotRepository timeSlotRepository;

        public GetAllSubscriptions(ISubscriptionRepository subscriptionRepository, ITimeSlotRepository timeSlotRepository) {
            this.subscriptionRepository = subscriptionRepository;
            this.timeSlotRepository = timeSlotRepository;
        }


        public IEnumerable<domain.Subscription> Execute() {
            return subscriptionRepository.GetAll().Select(sub => {
                sub.Place.OpenedTimes = timeSlotRepository.GetAllOfPlace(sub.PlaceId);
                return sub;
            }).ToList();
        }

    }

}