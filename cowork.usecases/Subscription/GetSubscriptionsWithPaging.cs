using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Subscription {

    public class GetSubscriptionsWithPaging : IUseCase<IEnumerable<domain.Subscription>> {

        private readonly ISubscriptionRepository subscriptionRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetSubscriptionsWithPaging(ISubscriptionRepository subscriptionRepository, int page, int amount) {
            this.subscriptionRepository = subscriptionRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Subscription> Execute() {
            return subscriptionRepository.GetAllWithPaging(Page, Amount);
        }

    }

}