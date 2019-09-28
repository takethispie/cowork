using cowork.domain;
using cowork.domain.Interfaces;
using cowork.test.InMemoryRepositories;
using NUnit.Framework;

namespace cowork.test.DbTests.Repositories {

    [TestFixture]
    public class RoomDbTest {

        [SetUp]
        public void Setup() {
            repo = new InMemoryRoomRepository();
            placeRepository = new InMemoryPlaceRepository();
            place = new Place(-1, "Bastille", true, true, true, 2, 3, 1);
            placeId = placeRepository.Create(place);
            place.Id = placeId;
            var room = new Room(-1, place.Id, "Room1", RoomType.Meeting);
            roomId = repo.Create(room);
        }
        

        private IRoomRepository repo;
        private IPlaceRepository placeRepository;
        private long roomId, placeId;
        private Place place;


        [Test]
        public void Create() {
            var room = new Room(-1, place.Id, "Room2", RoomType.Call);
            var newId = repo.Create(room);
            Assert.Greater(roomId, -1);
            repo.Delete(newId);
        }


        [Test]
        public void Delete() {
            var result = repo.Delete(roomId);
            Assert.AreEqual(true, result);
        }


        [Test]
        public void GetAll() {
            var result = repo.GetAll();
            Assert.NotNull(result);
        }


        [Test]
        public void GetById() {
            var result = repo.GetById(roomId);
            Assert.NotNull(result);
        }


        [Test]
        public void GetByName() {
            var result = repo.GetByName("Room1");
            Assert.NotNull(result);
        }


        [Test]
        public void Update() {
            var room = repo.GetById(roomId);
            Assert.NotNull(room);
            room.Name = "moulin rouge";
            //on simule la jointure faite dans les layer d'abstraction au dessus
            room.PlaceId = place.Id;
            var result = repo.Update(room) > -1;
            Assert.AreEqual(true, result);
        }

    }

}