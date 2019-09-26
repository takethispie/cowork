using System.Collections.Generic;
using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryTimeSlotRepository : ITimeSlotRepository {

        public List<TimeSlot> TimeSlots;


        public InMemoryTimeSlotRepository() {
            TimeSlots = new List<TimeSlot>();
        }
        
        public List<TimeSlot> GetAll() {
            return TimeSlots;
        }


        public TimeSlot GetById(long id) {
            return TimeSlots.Find(t => t.Id == id);
        }


        public List<TimeSlot> GetAllOfPlace(long placeId) {
            return TimeSlots.FindAll(t => t.PlaceId == placeId);
        }


        public List<TimeSlot> GetAllByPaging(int page, int amount) {
            return TimeSlots.Skip(page * amount).Take(amount).ToList();
        }


        public bool Delete(long id) {
            var item = TimeSlots.Find(b => b.Id == id);
            if (item == null) return false;
            TimeSlots.Remove(item);
            return true;
        }


        public long Create(TimeSlot timeSlot) {
            var id = TimeSlots.Count;
            timeSlot.Id = id;
            TimeSlots.Add(timeSlot);
            return id;
        }


        public long Update(TimeSlot item) {
            long id = -1;
            TimeSlots = TimeSlots.Select(i => {
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