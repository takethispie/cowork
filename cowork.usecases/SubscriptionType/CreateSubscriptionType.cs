using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class CreateSubscriptionType : IUseCase<long> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        public readonly domain.SubscriptionType SubscriptionType;

        public CreateSubscriptionType(ISubscriptionTypeRepository subscriptionTypeRepository, domain.SubscriptionType subscriptionType) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            SubscriptionType = subscriptionType;
        }


        public long Execute() {
            return subscriptionTypeRepository.Create(SubscriptionType);
        }

    }

}