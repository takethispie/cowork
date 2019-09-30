using cowork.domain.Interfaces;
using cowork.usecases.SubscriptionType;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.SubscriptionType {

    [TestFixture]
    public class DeleteSubscriptionTypeTest {

        [Test]
        public void ShouldDeleteSubscriptionType() {
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.Delete(0)).Returns(true);
            
            var res = new DeleteSubscriptionType(mockSubTypeRepo.Object, 0).Execute();
            Assert.IsTrue(res);
        }
        
        
        [Test]
        public void ShouldFailDeletingSubscriptionType() {
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.Delete(0)).Returns(false);
            
            var res = new DeleteSubscriptionType(mockSubTypeRepo.Object, 0).Execute();
            Assert.IsFalse(res);
        }

    }

}