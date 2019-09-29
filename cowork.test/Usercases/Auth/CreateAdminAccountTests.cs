using System;
using System.Collections.Generic;
using System.Security.Claims;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.services.Login;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Auth {

    public class CreateAdminAccountTests {

        [Test]
        public void ShouldCreateAdminAccount() {
            var user = new User(0, "alexandre", "felix", false, UserType.Admin);
            var login = new Login("admin", "admin@admin.com", 0);
            
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Create(login)).Returns(0);
            
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(mock => mock.Create(It.IsAny<User>())).Returns(0);
            

            var createAdmin = new CreateAdminAccount(mockLoginrepo.Object, mockUserRepo.Object, user, "admin@admin.com", "admin");
            var res = createAdmin.Execute();
            Assert.AreEqual(0, res);
        }


        [Test]
        public void ShouldFailCreatingAdminAccountOnEmptyPassword() {
            var ex = Assert.Throws<Exception>(() => {
                var user = new User(0, "alexandre", "felix", false, UserType.Admin);
                var login = new Login("", "admin@admin.com", 0);
            
                var mockLoginrepo = new Mock<ILoginRepository>();
                mockLoginrepo.Setup(mock => mock.Create(login)).Returns(0);
            
                var mockUserRepo = new Mock<IUserRepository>();
                mockUserRepo.Setup(mock => mock.Create(It.IsAny<User>())).Returns(0);
            

                var createAdmin = new CreateAdminAccount(mockLoginrepo.Object, mockUserRepo.Object, user, "admin@admin.com", "");
                var res = createAdmin.Execute();
            });
            Assert.AreEqual("mot de passe invalide", ex.Message);
            
        }
        
        
        [Test]
        public void ShouldFailCreatingAdminOnPersistenceError() {
            Assert.Throws<Exception>(() => {
                var user = new User(0, "alexandre", "felix", false, UserType.Admin);
                var login = new Login("adfsf", "admin@admin.com", 0);
            
                var mockLoginrepo = new Mock<ILoginRepository>();
                mockLoginrepo.Setup(mock => mock.Create(login)).Returns(0);
            
                var mockUserRepo = new Mock<IUserRepository>();
                mockUserRepo.Setup(mock => mock.Create(It.IsAny<User>())).Returns(0);
            

                var createAdmin = new CreateAdminAccount(mockLoginrepo.Object, mockUserRepo.Object, user, "admin@admin.com", "");
                var res = createAdmin.Execute();
            });
            
        }
        

    }

}