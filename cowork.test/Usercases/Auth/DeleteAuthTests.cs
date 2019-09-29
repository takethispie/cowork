using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Auth {

    [TestFixture]
    public class DeleteAuthTests {

        [Test]
        public void ShouldDeleteAuth() {
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Delete(0)).Returns(true);
            
            var res = new DeleteAuth(mockLoginrepo.Object, 0).Execute();
            Assert.IsTrue(res);
        }


        [Test]
        public void ShouldFailDeletingAuth() {
            var mockLoginrepo = new Mock<ILoginRepository>();
            mockLoginrepo.Setup(mock => mock.Delete(It.Is<long>(v => v != 0))).Returns(false);

            var res = new DeleteAuth(mockLoginrepo.Object, 10).Execute();
            Assert.IsFalse(res);
        }

    }

}