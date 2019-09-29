using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class UpdateSubscription : IUseCase<long> {

        private readonly ISubscriptionRepository subscriptionRepository;
        public readonly domain.Subscription Subscription;

        public UpdateSubscription(ISubscriptionRepository subscriptionRepository, domain.Subscription subscription) {
            this.subscriptionRepository = subscriptionRepository;
            Subscription = subscription;
        }


        public long Execute() {
            return subscriptionRepository.Update(Subscription);
        }

    }

}