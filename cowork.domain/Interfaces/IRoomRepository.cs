using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface IRoomRepository {

        List<Room> GetAll();

        Room GetById(long id);
        
        List<Room> GetAllFromPlace(long placeId);
        List<Room> GetAllWithPaging(int page, int amount);

        long Create(Room room);

        bool Delete(long id);

        long Update(Room room);

    }

}