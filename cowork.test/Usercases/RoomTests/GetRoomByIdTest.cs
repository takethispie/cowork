using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Room;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.RoomTests {

    [TestFixture]
    public class GetRoomByIdTest {

        [Test]
        public void ShouldGetRoomById() {
            var output = new Room(0,0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.GetById(0)).Returns(output);

            var res = new GetRoomById(mockRoomRepo.Object, 0).Execute();
            Assert.NotNull(res);
            Assert.AreEqual("test", res.Name);
            Assert.AreEqual(RoomType.Call, res.Type);
            Assert.AreEqual(0, res.PlaceId);
        }


        [Test]
        public void ShouldFailGettingRoomById() {
            var output = new Room(0,0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.GetById(0)).Returns(output);

            var res = new GetRoomById(mockRoomRepo.Object, 1).Execute();
            Assert.IsNull(res);
        }

    }

}