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


        public List<string> GetEmailListOfSoonToBeExpiredSubscription(int threshold) {
            var soonToExpire = subscriptionRepository.GetAll()
                .Where(sub => sub.FixedContract && isSubscriptionExpiringSoon(threshold, sub));
            var userIdsWithExpiringSub = soonToExpire
                .Where(sub => userRepository.GetById(sub.ClientId).Type == UserType.User)
                .Select(sub => sub.ClientId);
            var emailsOfUserWithExpiringSub = userIdsWithExpiringSub.Select(id => loginRepository.ByUserId(id).Email);
            return emailsOfUserWithExpiringSub.ToList();
        }


        private static bool isSubscriptionExpiringSoon(int threshold, Subscription sub) {
            var expiration = sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth);
            var limitBeforeNotification = DateTime.Today.AddDays(threshold);
            return expiration < limitBeforeNotification;
        }

    }

}