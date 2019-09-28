using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Datamappers;
using coworktest.InMemoryRepositories;
using NUnit.Framework;

namespace coworktest.DbTests.Repositories {

    public class UserDbTest {

        private string connection;
        private SqlDataMapper<User> datamapper;
        private IUserRepository repo;
        private long testUser;


        [SetUp]
        public void Setup() {
            repo = new InMemoryUserRepository();
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