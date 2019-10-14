using System;
using System.Collections.Generic;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.test.InMemoryRepositories {

    public class InMemoryMealBookingRepository : IMealBookingRepository {

        public List<MealBooking> MealBookings;


        public InMemoryMealBookingRepository() {
            MealBookings = new List<MealBooking>();
        }
        
        public List<MealBooking> GetAll() {
            return MealBookings;
        }


        public List<MealBooking> GetAllFromUser(long userId) {
            return MealBookings.FindAll(booking => booking.UserId == userId);
        }


        public List<MealBooking> GetAllFromDateAndPlace(DateTime date, long placeId) {
            return new List<MealBooking>();
        }


        //cant use date 
        public List<MealBooking> GetAllWithPaging(int page, int amount) {
            return MealBookings.Skip(page * amount).Take(amount).ToList();
        }


        public MealBooking GetById(long id) {
            return MealBookings.Find(b => b.Id == id);
        }


        public bool Delete(long id) {
            var booking = MealBookings.Find(b => b.Id == id);
            if (booking == null) return false;
            MealBookings.Remove(booking);
            return true;
        }


        public long Update(MealBooking item) {
            long id = -1;
            MealBookings = MealBookings.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public long Create(MealBooking meal) {
            var id = MealBookings.Count;
            meal.Id = id;
            MealBookings.Add(meal);
            return id;
        }

    }

}