using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence;
using coworkpersistence.Repositories;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class LoginDbTest {

        [SetUp]
        public void Setup() {
            loginRepository = new InMemoryLoginRepository();
            userRepository = new InMemoryUserRepository();
            userId = userRepository.Create(new User(-1, "Alexandre", "testLogin", false, UserType.User));
        }
        

        private ILoginRepository loginRepository;
        private IUserRepository userRepository;
        private long loginId, userId;


        [Test]
        public void Create() {
            const string pass = "test";
            PasswordHashing.CreatePasswordHash(pass, out var passwordHash, out var passwordSalt);
            var login = new Login(-1, passwordHash, passwordSalt, "test", userId);
            var newLoginId = loginRepository.Create(login);
            Assert.Greater(newLoginId, -1);
            var result = loginRepository.Auth("test", "test");
            Assert.Greater(result, -1);
            Assert.AreEqual(userId, result);
            loginRepository.Delete(newLoginId);
        }

    }

}