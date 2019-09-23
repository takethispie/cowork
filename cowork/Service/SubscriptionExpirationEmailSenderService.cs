using System.Diagnostics;
using coworkdomain;
using coworkdomain.Cowork.Interfaces;
using Hangfire;

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