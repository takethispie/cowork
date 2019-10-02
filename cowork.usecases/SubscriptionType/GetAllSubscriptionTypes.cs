using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.SubscriptionType {

    public class GetAllSubscriptionTypes : IUseCase<IEnumerable<domain.SubscriptionType>> {

        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;
        
        public GetAllSubscriptionTypes(ISubscriptionTypeRepository subscriptionTypeRepository) {
            this.subscriptionTypeRepository = subscriptionTypeRepository;
        }


        public IEnumerable<domain.SubscriptionType> Execute() {
            return subscriptionTypeRepository.GetAll();
        }

    }

}