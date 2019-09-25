using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.Repositories;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest {

    public class UserDbTest {

        private string connection;
        private SqlDataMapper<User> datamapper;
        private IUserRepository repo;
        private long testUser;


        [OneTimeSetUp]
        public void OneTimeSetup() {
            connection = "Host=localhost;Database=cowork;Username=postgres;Password=ariba1";
            repo = new InMemoryUserRepository();
        }


        [SetUp]
        public void Setup() {
            testUser = repo.Create(new User(-1, "Alexandre", "Felix", false, UserType.User));
        }


        [Test]
        public void CreateUser() {
            var user = new User(-1, "jean", "jean", true, UserType.User);
            var result = repo.Create(user) > -1;
            Assert.AreEqual(true, result);
        }


        [Test]
        public void FailToGetUserUsingWrongId() {
            var user = repo.GetById(long.MaxValue);
            Assert.IsNull(user);
        }


        [Test]
        public void GetUserById() {
            var user = repo.GetById(testUser);
            Assert.NotNull(user);
        }


        [Test]
        public void CreateAndDeleteUser() {
            var user = new User(-1, "todelete", "todelete", false, UserType.User);
            var id = repo.Create(user);
            var result = repo.DeleteById(id);
            Assert.AreEqual(true, result);
        }


        [Test]
        public void UpdateUser() {
            var user = repo.GetById(testUser);
            user.FirstName = "jeremy";
            var result = repo.Update(user) > -1;
            Assert.AreEqual(true, result);
            var updatedUser = repo.GetById(testUser);
            Assert.AreEqual("jeremy", updatedUser.FirstName);
        }
    }

}