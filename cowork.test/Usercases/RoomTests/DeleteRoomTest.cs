using cowork.domain.Interfaces;
using cowork.usecases.Room;
using Moq;
using NUnit.Framework;

namespace cowork.test.Usercases.RoomTests {

    [TestFixture]
    public class DeleteRoomTest {

        [Test]
        public void ShouldDeleteRoom() {
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Delete(0)).Returns(true);
            var res = new DeleteRoom(mockRoomRepo.Object, 0).Execute();
            Assert.IsTrue(res);
        }


        [Test]
        public void ShouldFailDeletingRoom() {
            var mockRoomRepo = new Mock<IRoomRepository>();
            mockRoomRepo.Setup(m => m.Delete(0)).Returns(false);
            var res = new DeleteRoom(mockRoomRepo.Object, 0).Execute();
            Assert.IsFalse(res);
        }
    }

}