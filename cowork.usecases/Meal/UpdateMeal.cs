using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class UpdateMeal : IUseCase<long> {

        private readonly IMealRepository mealRepository;
        public readonly domain.Meal Meal;

        public UpdateMeal(IMealRepository mealRepository, domain.Meal meal) {
            this.mealRepository = mealRepository;
            Meal = meal;
        }


        public long Execute() {
            return mealRepository.Update(Meal);
        }

    }

}