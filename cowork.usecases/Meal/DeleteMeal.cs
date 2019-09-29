using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class DeleteMeal : IUseCase<bool> {

        private readonly IMealRepository mealRepository;
        public readonly long Id;


        public DeleteMeal(IMealRepository mealRepository, long id) {
            this.mealRepository = mealRepository;
            Id = id;
        }


        public bool Execute() {
            return mealRepository.Delete(Id);
        }

    }

}