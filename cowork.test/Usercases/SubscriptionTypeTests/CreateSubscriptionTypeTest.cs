using cowork.domain.Interfaces;
using cowork.usecases.SubscriptionType;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.SubscriptionTypeTests {

    [TestFixture]
    public class CreateSubscriptionTypeTest {

        [Test]
        public void ShouldCreateSubscriptionType() {
            var subType = new domain.SubscriptionType(0, "test", 8, 5, 3, 30, 25, 200, 280, "description");
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.Create(subType)).Returns(0);
            
            var res = new CreateSubscriptionType(mockSubTypeRepo.Object, subType).Execute();
            Assert.AreEqual(0, res);
        }
        
        
        [Test]
        public void ShouldFailCreatingSubscriptionType() {
            var subType = new domain.SubscriptionType(0, "test", 8, 5, 3, 30, 25, 200, 280, "description");
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.Create(It.IsAny<domain.SubscriptionType>())).Returns(-1);
            
            var res = new CreateSubscriptionType(mockSubTypeRepo.Object, subType).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}