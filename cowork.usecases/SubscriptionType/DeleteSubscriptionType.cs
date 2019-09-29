using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class DeleteSubscriptionType : IUseCase<bool> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        public readonly long Id;

        public DeleteSubscriptionType(ISubscriptionTypeRepository subscriptionTypeRepository, long id) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            Id = id;
        }


        public bool Execute() {
            return subscriptionTypeRepository.Delete(Id);
        }

    }

}