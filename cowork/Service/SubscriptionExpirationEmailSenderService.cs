using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.Service {

    public class SubscriptionExpirationEmailSenderService : IScheduledService {

        private SubscriptionExpirationManager subscriptionExpirationManager;


        public SubscriptionExpirationEmailSenderService(IUserRepository userRepository, ILoginRepository loginRepository, ISubscriptionRepository subscriptionRepository) {
            subscriptionExpirationManager = new SubscriptionExpirationManager(userRepository, subscriptionRepository, loginRepository);
            
        }


        public void Process() {
            
        }
    }

}