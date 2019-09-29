using cowork.domain.Interfaces;
using cowork.usecases.Meal.Models;

namespace cowork.usecases.Meal {

    public class CreateMeal : IUseCase<long> {

        private readonly IMealRepository mealRepository;
        public readonly domain.Meal Meal;


        public CreateMeal(IMealRepository mealRepository, MealInput meal) {
            this.mealRepository = mealRepository;
            Meal = new domain.Meal(meal.Date, meal.Description, meal.PlaceId);
        }


        public long Execute() {
            return mealRepository.Create(Meal);
        }

    }

}