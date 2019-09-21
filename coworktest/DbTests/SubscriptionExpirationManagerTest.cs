using System;
using System.Linq;
using cowork;
using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest.DbTests {

    [TestFixture]
    public class SubscriptionExpirationManagerTest {

        private IUserRepository userRepository;
        private ISubscriptionRepository subscriptionRepository;
        private ILoginRepository loginRepository;
        private SubscriptionType subscriptionType;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            userRepository = new InMemoryUserRepository();
            subscriptionRepository = new InMemorySubscriptionRepository();
            loginRepository = new InMemoryLoginRepository();
            subscriptionType = new SubscriptionType(1, "s", 8, 1,1,1,1,1,1, "s");
            var user = new User(-1, "1", "1", false, UserType.User);
            var userId = userRepository.Create(user);
            var sub = new Subscription(-1, 1, userId, DateTime.Now.AddMonths(-8), 1, true) {Type = subscriptionType};
            subscriptionRepository.Create(sub);
            PasswordHashing.CreatePasswordHash("test", out var hash, out var salt);
            var login = new Login(-1, hash, salt, "test@test.com", userId);
            loginRepository.Create(login);
            
            user = new User(-1, "1", "1", false, UserType.Staff);
            userId = userRepository.Create(user);
            sub = new Subscription(-1, 1, userId, DateTime.Now.AddMonths(-8), 1, true) {Type = subscriptionType};
            subscriptionRepository.Create(sub);
            login = new Login(-1, hash, salt, "test2@test.com", userId);
            loginRepository.Create(login);
            
            user = new User(-1, "1", "1", false, UserType.User);
            userId = userRepository.Create(user);
            sub = new Subscription(-1, 1, userId, DateTime.Now.AddMonths(-7).AddDays(-10), 1, true) {Type = subscriptionType};
            subscriptionRepository.Create(sub);
            login = new Login(-1, hash, salt, "test3@test.com", userId);
            loginRepository.Create(login);
        }


        [Test]
        public void HasOneExpiringSubscriptionUserOn3DaysNotice() {
            var manager = new SubscriptionExpirationManager(userRepository, subscriptionRepository, loginRepository);
            var res = manager.GetEmailListOfSoonToBeExpiredSubscription(3);
            Assert.NotNull(res);
            Assert.AreEqual(1, res.Count);
        }
        
        
        [Test]
        public void HasOneExpiringSubscriptionUserOnOneMonthNotice() {
            var manager = new SubscriptionExpirationManager(userRepository, subscriptionRepository, loginRepository);
            var res = manager.GetEmailListOfSoonToBeExpiredSubscription(30);
            Assert.NotNull(res);
            Assert.AreEqual(2, res.Count);
            Assert.IsTrue(res.Any(email => email == "test@test.com"));
            Assert.IsTrue(res.Any(email => email == "test3@test.com"));
            Assert.IsTrue(res.All(email => email != "test2@test.com"));
        }
    }

}