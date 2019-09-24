using coworkdomain;
using coworkdomain.Cowork.Interfaces;
using Hangfire;

namespace cowork.Service {

    public class ExpiredSubscriptionDeletionService : IScheduledService{

        private SubscriptionExpirationManager subscriptionExpirationManager;
        private ISubscriptionRepository subscriptionRepository;


        public ExpiredSubscriptionDeletionService(IUserRepository userRepository, ILoginRepository loginRepository, ISubscriptionRepository subscriptionRepository) {
            subscriptionExpirationManager = new SubscriptionExpirationManager(userRepository, subscriptionRepository, loginRepository);
            this.subscriptionRepository = subscriptionRepository;
            RecurringJob.AddOrUpdate(() => Process(), Cron.Minutely);
        }


        public void Process() {
            var expired = subscriptionExpirationManager.GetAllExpiredSubscriptions();
            foreach (var subscription in expired) {
                subscriptionRepository.Delete(subscription.Id);
            }
        }

    }
}