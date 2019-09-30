using cowork.domain.Interfaces;
using cowork.usecases.Subscription;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Subscription {

    [TestFixture]
    public class DeleteSubscriptionTest {

        [Test]
        public void ShouldDeleteSubscription() {
            var mockSubRepo = new Mock<ISubscriptionRepository>();
            mockSubRepo.Setup(m => m.Delete(0)).Returns(true);
            
            var delete = new DeleteSubscription(mockSubRepo.Object, 0).Execute();
            Assert.IsTrue(delete);
        }

    }

}