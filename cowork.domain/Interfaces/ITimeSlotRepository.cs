using System.Collections.Generic;

namespace cowork.domain.Interfaces {

    public interface ITimeSlotRepository {

        List<TimeSlot> GetAll();

        TimeSlot GetById(long id);

        List<TimeSlot> GetAllOfPlace(long placeId);
        List<TimeSlot> GetAllByPaging(int page, int amount);

        bool Delete(long id);

        long Create(TimeSlot timeSlot);

        long Update(TimeSlot timeSlot);

    }

}