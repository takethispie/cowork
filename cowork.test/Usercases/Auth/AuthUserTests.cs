using System;
using System.Collections.Generic;
using System.Security.Claims;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.services.Login;
using cowork.test.InMemoryRepositories;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Auth {

    [TestFixture]
    public class AuthUserTests {

        [Test]
        public void ShouldAuth() {
            var user = new User(0, "alexandre", "felix", false, UserType.User);
            var sub = new domain.Subscription(0, 0, DateTime.Now, 0, true);
            var subType = new domain.SubscriptionType(0, "resident", 8, 3, 2, 20, 15, 20, 25, "test");
            sub.Type = subType;
            var mockTokenhandler = new Mock<ITokenHandler>();
            mockTokenhandler.Setup(mock => mock.EncryptToken(It.IsAny<List<Claim>>())).Returns("authtokens");
            
            
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Auth("test", "123")).Returns(0);
            
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(mock => mock.GetById(It.IsAny<long>())).Returns(user);
            
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(mock => mock.GetOfUser(0)).Returns(sub);
            
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.GetById(0)).Returns(subType);
            
            var doAuth = new AuthUser(mockLoginrepo.Object, mockUserRepo.Object, mockSubRepo.Object, 
                mockTokenhandler.Object, mockSubTypeRepo.Object, 
                new CredentialsInput {Email = "test", Password = "123"});

            var res = doAuth.Execute();
            Assert.AreEqual(user, res.user);
            Assert.AreEqual("felix", res.user.LastName);
            Assert.AreEqual("authtokens", res.auth_token);
            Assert.AreEqual("resident", res.sub.Type.Name);
        }

    }

}