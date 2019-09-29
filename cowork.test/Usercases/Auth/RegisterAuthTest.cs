using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Auth {

    [TestFixture]
    public class RegisterAuthTest {

        [Test]
        public void ShouldRegisterAuth() {
            var user = new User(0, "alexandre", "felix", true, UserType.User);
            var registration = new UserRegistrationInput() {Email = "test@test.com", Password = "test", User = user};
            
            var mockLoginRepo = new Mock<ILoginRepository>();
            mockLoginRepo.Setup(mock => mock.Create(It.IsAny<Login>())).Returns(0);
            
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(mock => mock.Create(It.IsAny<User>())).Returns(0);
            mockUserRepo.Setup(mock => mock.DeleteById(0)).Returns(true);
            
            var res = new RegisterAuth(mockLoginRepo.Object, mockUserRepo.Object, registration).Execute();
            Assert.AreEqual("alexandre", res.User.FirstName);
            Assert.AreEqual("felix", res.User.LastName);
            Assert.AreEqual(true, res.User.IsAStudent);
            Assert.AreEqual(UserType.User, res.User.Type);
        }

    }

}