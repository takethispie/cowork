using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemoryRoomRepository : IRoomRepository {

        public List<Room> Rooms;


        public InMemoryRoomRepository() {
            Rooms = new List<Room>();
        }
        
        public List<Room> GetAll() {
            return Rooms;
        }


        public Room GetById(long id) {
            return Rooms.Find(r => r.Id == id);
        }


        public Room GetByName(string name) {
            return Rooms.Find(r => r.Name == name);
        }


        public List<Room> GetAllFromPlace(long placeId) {
            return Rooms.Where(r => r.PlaceId == placeId).ToList();
        }


        public List<Room> GetAllWithPaging(int page, int amount) {
            return Rooms.Skip(page * amount).Take(amount).ToList();
        }


        public long Create(Room room) {
            var id = Rooms.Count;
            room.Id = id;
            Rooms.Add(room);
            return id;
        }


        public bool Delete(long id) {
            var item = Rooms.Find(r => r.Id == id);
            if (item == null) return false;
            Rooms.Remove(item);
            return true;
        }


        public long Update(Room item) {
            long id = -1;
            Rooms = Rooms.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }
    }
}