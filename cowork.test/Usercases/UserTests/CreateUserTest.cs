using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.User;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.UserTests {

    [TestFixture]
    public class CreateUserTest {

        [Test]
        public void ShouldCreateUser() {
            var user = new User("alex", "felix", false, UserType.User);
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(m => m.Create(user)).Returns(0);

            var res = new CreateUser(mockUserRepo.Object, user).Execute();
            Assert.AreEqual(0, res);
        }

        [Test]
        public void ShouldFailCreatingUser() {
            var user = new User("alex", "felix", false, UserType.User);
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(m => m.Create(user)).Returns(-1);

            var res = new CreateUser(mockUserRepo.Object, user).Execute();
            Assert.AreEqual(-1, res);
        }
    }

}