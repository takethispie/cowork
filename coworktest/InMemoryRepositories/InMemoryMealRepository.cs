using System;
using System.Collections.Generic;
using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;

namespace coworktest.InMemoryRepositories {

    public class InMemoryMealRepository : IMealRepository {

        public List<Meal> Meals;


        public InMemoryMealRepository() {
            Meals = new List<Meal>();
        }
        
        public List<Meal> GetAll() {
            return Meals;
        }


        public List<Meal> GetAllFromPlace(long id) {
            return Meals.Where(m => m.PlaceId == id).ToList();
        }


        public List<Meal> GetAllFromDateAndPlace(DateTime date, long placeId) {
            return Meals.Where(m => m.PlaceId == placeId && m.Date == date).ToList();
        }


        public List<Meal> GetAllFromPlaceStartingAtDate(long placeId, DateTime date) {
            return Meals.Where(m => m.PlaceId == placeId && m.Date >= date).ToList();
        }


        public List<Meal> GetAllByPaging(int page, int amount) {
            return Meals.Skip(page * amount).Take(amount).ToList();
        }


        public Meal GetById(long id) {
            return Meals.Find(m => m.Id == id);
        }


        public bool Delete(long id) {
            var item = Meals.Find(i => i.Id == id);
            if (item == null) return false;
            Meals.Remove(item);
            return true;
        }


        public long Update(Meal item) {
            long id = -1;
            Meals = Meals.Select(i => {
                if (i.Id == item.Id) {
                    id = item.Id;
                    return item;
                }
                id = -1;
                return i;
            }).ToList();
            return id;
        }


        public long Create(Meal meal) {
            var id = Meals.Count;
            meal.Id = id;
            Meals.Add(meal);
            return id;
        }

    }

}