using cowork.domain.Interfaces;
using cowork.usecases.SubscriptionType;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.SubscriptionTypeTests {

    [TestFixture]
    public class GetSubscriptionTypeByIdTest {

        [Test]
        public void ShouldGetSubscriptionById() {
            var subType = new domain.SubscriptionType(0, "test", 8, 5, 3, 30, 25, 200, 280, "description");
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.GetById(0)).Returns(subType);
            
            var res = new GetSubscriptionTypeById(mockSubTypeRepo.Object, 0).Execute();
            Assert.NotNull(res);
            Assert.AreEqual("test",res.Name);
        }
        
        
        [Test]
        public void ShouldFailGettingSubscriptionById() {
            var subType = new domain.SubscriptionType(0, "test", 8, 5, 3, 30, 25, 200, 280, "description");
            var mockSubTypeRepo = new Mock<ISubscriptionTypeRepository>();
            mockSubTypeRepo.Setup(m => m.GetById(0)).Returns(subType);
            
            var res = new GetSubscriptionTypeById(mockSubTypeRepo.Object, 1).Execute();
            Assert.IsNull(res);
        }

    }

}