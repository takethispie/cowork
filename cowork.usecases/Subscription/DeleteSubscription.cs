using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class DeleteSubscription : IUseCase<bool> {

        private readonly ISubscriptionRepository subscriptionRepository;
        public readonly long Id;

        public DeleteSubscription(ISubscriptionRepository subscriptionRepository, long id) {
            this.subscriptionRepository = subscriptionRepository;
            Id = id;
        }


        public bool Execute() {
            return subscriptionRepository.Delete(Id);
        }

    }

}