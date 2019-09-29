using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class GetSubscriptionTypeById : IUseCase<domain.SubscriptionType> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        public readonly long Id;

        public GetSubscriptionTypeById(ISubscriptionTypeRepository subscriptionTypeRepository, long id) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            Id = id;
        }


        public domain.SubscriptionType Execute() {
            return subscriptionTypeRepository.GetById(Id);
        }

    }

}