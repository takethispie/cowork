using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.AuthTests {

    [TestFixture]
    public class UpdateAuthTests {

        [Test]
        public void ShouldUpdateAuth() {
            var input = new LoginInput() {Email = "test@test.com", Password = "test", UserId = 0};
            
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Create(It.IsAny<Login>())).Returns(0);

            var res = new UpdateAuth(mockLoginrepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }

    }

}