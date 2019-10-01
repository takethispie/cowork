using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Room;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Room {

    [TestFixture]
    public class UpdateRoomTest {

        [Test]
        public void ShouldUpdateRoom() {
            var input = new domain.Room(0,0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Update(input)).Returns(0);
            var res = new UpdateRoom(mockRoomRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }


        [Test]
        public void ShouldFailUpdatingRoom() {
            var input = new domain.Room(0,0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Update(It.IsAny<domain.Room>())).Returns(-1);
            var res = new UpdateRoom(mockRoomRepo.Object, input).Execute();
            Assert.AreEqual(-1, res);
        }

    }

}