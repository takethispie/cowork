using System;
using cowork.domain.Interfaces;
using cowork.usecases.Meal.Models;

namespace cowork.usecases.Meal {

    public class CreateMeal : IUseCase<long> {

        private readonly IMealRepository mealRepository;
        public readonly domain.Meal Meal;


        public CreateMeal(IMealRepository mealRepository, MealInput meal) {
            this.mealRepository = mealRepository;
            var date = new DateTime(meal.Date.Year, meal.Date.Month, meal.Date.Day);
            Meal = new domain.Meal(date, meal.Description, meal.PlaceId);
        }


        public long Execute() {
            return mealRepository.Create(Meal);
        }

    }

}