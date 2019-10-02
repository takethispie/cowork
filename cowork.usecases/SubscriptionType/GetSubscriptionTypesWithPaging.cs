using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class GetSubscriptionTypesWithPaging : IUseCase<IEnumerable<domain.SubscriptionType>> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetSubscriptionTypesWithPaging(ISubscriptionTypeRepository subscriptionTypeRepository, int page, int amount) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.SubscriptionType> Execute() {
            return subscriptionTypeRepository.GetAllWithPaging(Page, Amount);
        }

    }

}