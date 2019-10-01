using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Room;
using cowork.usecases.Room.Models;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.Room {

    [TestFixture]
    public class CreateRoomTest {

        [Test]
        public void ShouldCreateRoom() {
            var input = new CreateRoomInput(0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Create(It.Is<domain.Room>(r => r.Name == "test"))).Returns(0);
            var res = new CreateRoom(mockRoomRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }


        [Test]
        public void ShouldFailCreatingRoom() {
            var input = new CreateRoomInput(0, "test", RoomType.Call);
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Create(It.Is<domain.Room>(r => r.Name == "test"))).Returns(0);
            var res = new CreateRoom(mockRoomRepo.Object, input).Execute();
            Assert.AreEqual(0, res);
        }

    }

}