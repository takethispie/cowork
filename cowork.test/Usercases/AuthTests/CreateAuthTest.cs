using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.AuthTests {

    [TestFixture]
    public class CreateAuthTest {

        [Test]
        public void ShouldCreateAuth() {
            var loginInput = new LoginInput {Id = 0, Email = "test@test.com", Password = "test", UserId = 0};
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Create(It.IsAny<Login>())).Returns(0);

            var res = new CreateAuth(mockLoginrepo.Object, loginInput).Execute();
            Assert.AreEqual(0, res);
        }

    }

}