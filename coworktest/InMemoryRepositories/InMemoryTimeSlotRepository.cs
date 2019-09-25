using System.Collections.Generic;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTimeSlotRepository : ITimeSlotRepository {

        public List<TimeSlot> TimeSlots;


        public InMemoryTimeSlotRepository() {
            TimeSlots = new List<TimeSlot>();
        }
        
        public List<TimeSlot> GetAll() {
            throw new System.NotImplementedException();
        }


        public TimeSlot GetById(long id) {
            throw new System.NotImplementedException();
        }


        public List<TimeSlot> GetAllOfPlace(long placeId) {
            throw new System.NotImplementedException();
        }


        public List<TimeSlot> GetAllByPaging(int page, int amount) {
            throw new System.NotImplementedException();
        }


        public bool Delete(long id) {
            throw new System.NotImplementedException();
        }


        public long Create(TimeSlot timeSlot) {
            throw new System.NotImplementedException();
        }


        public long Update(TimeSlot timeSlot) {
            throw new System.NotImplementedException();
        }

    }

}