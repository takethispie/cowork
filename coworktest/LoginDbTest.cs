using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence;
using coworkpersistence.Repositories;
using NUnit.Framework;

namespace coworktest {

    [TestFixture]
    public class LoginDbTest {

        private ILoginRepository loginRepository;
        private IUserRepository userRepository;
        private long loginId, userId;
        private string connection;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            loginRepository = new LoginRepository(connection);
            userRepository = new UserRepository(connection);
            userId = userRepository.Create(new User(-1, "Alexandre", "testLogin", "test", false, UserType.User));
        }


        [OneTimeTearDown]
        public void OneTimeTearDown() {
            userRepository.DeleteById(userId);
        }


        [SetUp]
        public void Setup() {
            
        }


        [TearDown]
        public void TearDown() {
            
        }


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