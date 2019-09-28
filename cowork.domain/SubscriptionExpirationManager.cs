using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.domain {

    public class SubscriptionExpirationManager {

        private IUserRepository userRepository;
        private ISubscriptionRepository subscriptionRepository;
        private ILoginRepository loginRepository;


        public SubscriptionExpirationManager(IUserRepository userRepository, 
                                             ISubscriptionRepository subscriptionRepository, 
                                             ILoginRepository loginRepository) {
            this.userRepository = userRepository;
            this.subscriptionRepository = subscriptionRepository;
            this.loginRepository = loginRepository;
        }


        public IEnumerable<string> GetEmailListOfSoonToBeExpiredSubscription(int threshold) {
            var soonToExpire = subscriptionRepository.GetAll()
                .Where(sub => sub.FixedContract && IsSubscriptionExpiringSoon(threshold, sub));
            var userIdsWithExpiringSub = soonToExpire
                .Where(sub => userRepository.GetById(sub.ClientId).Type == UserType.User)
                .Select(sub => sub.ClientId);
            var emailsOfUserWithExpiringSub = userIdsWithExpiringSub.Select(id => loginRepository.ByUserId(id).Email);
            return emailsOfUserWithExpiringSub.ToList();
        }


        public IEnumerable<Subscription> GetAllExpiredSubscriptions() {
            return subscriptionRepository.GetAll()
                .Where(sub => sub.FixedContract && IsExpired(sub, DateTime.Today));
        }


        private static bool IsExpired(Subscription sub, DateTime now) {
            return sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth) < DateTime.Today;
        }


        private static bool IsSubscriptionExpiringSoon(int daysThreshold, Subscription sub) {
            var expiration = sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth);
            var limitBeforeNotification = DateTime.Today.AddDays(daysThreshold);
            return expiration < limitBeforeNotification;
        }

    }

}