using System;
using System.Collections.Generic;
using System.Linq;
using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace cowork {

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
                .Where(sub => sub.FixedContract && IsSubscriptionExpiringSoon(1, sub));
        }


        private static bool IsSubscriptionExpiringSoon(int daysThreshold, Subscription sub) {
            var expiration = sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth);
            var limitBeforeNotification = DateTime.Today.AddDays(daysThreshold);
            return expiration < limitBeforeNotification;
        }

    }

}