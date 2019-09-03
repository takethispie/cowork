using System.Collections.Generic;

namespace coworkdomain.Cowork.Interfaces {

    public interface ITimeSlotRepository {

        List<TimeSlot> GetAll();

        TimeSlot GetById(long id);

        List<TimeSlot> GetAllOfPlace(long placeId);

        bool Delete(long id);

        long Create(TimeSlot timeSlot);

        long Update(TimeSlot timeSlot);

    }

}