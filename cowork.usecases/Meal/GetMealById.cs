using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class GetMealById : IUseCase<domain.Meal> {

        private readonly IMealRepository mealRepository;
        public readonly long Id;

        public GetMealById(IMealRepository mealRepository, long id) {
            this.mealRepository = mealRepository;
            Id = id;
        }


        public domain.Meal Execute() {
            return mealRepository.GetById(Id);
        }

    }

}