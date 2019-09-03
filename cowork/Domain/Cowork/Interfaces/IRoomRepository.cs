using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface IRoomRepository {

        List<Room> GetAll();

        Room GetById(long id);

        Room GetByName(string name);

        List<Room> GetAllFromPlace(long placeId);

        long Create(Room room);

        bool Delete(long id);

        long Update(Room room);

    }

}