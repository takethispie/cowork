using cowork.domain.Interfaces;
using cowork.usecases.Subscription.Models;

namespace cowork.usecases.Subscription {

    public class CreateSubscription : IUseCase<long> {

        private readonly ISubscriptionRepository subscriptionRepository;
        public readonly CreateSubscriptionInput Input;

        public CreateSubscription(ISubscriptionRepository subscriptionRepository, CreateSubscriptionInput input) {
            this.subscriptionRepository = subscriptionRepository;
            Input = input;
        }


        public long Execute() {
            var sub = new domain.Subscription(Input.TypeId, Input.ClientId, Input.LatestRenewal, Input.PlaceId,
                Input.FixedContract);
            return subscriptionRepository.Create(sub);
        }

    }

}