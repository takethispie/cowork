using System.Collections.Generic;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryRoomRepository : IRoomRepository {

        public List<Room> rooms;


        public InMemoryRoomRepository() {
            rooms = new List<Room>();
        }
        
        public List<Room> GetAll() {
            throw new System.NotImplementedException();
        }


        public Room GetById(long id) {
            throw new System.NotImplementedException();
        }


        public Room GetByName(string name) {
            throw new System.NotImplementedException();
        }


        public List<Room> GetAllFromPlace(long placeId) {
            throw new System.NotImplementedException();
        }


        public List<Room> GetAllWithPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public long Create(Room room) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Update(Room room) {
            throw new System.NotImplementedException();
        }

    }

}