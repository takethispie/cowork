using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class UpdateSubscriptionType : IUseCase<long> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        public readonly domain.SubscriptionType SubscriptionType;

        public UpdateSubscriptionType(ISubscriptionTypeRepository subscriptionTypeRepository, domain.SubscriptionType subscriptionType) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            SubscriptionType = subscriptionType;
        }


        public long Execute() {
            return subscriptionTypeRepository.Update(SubscriptionType);
        }

    }

}