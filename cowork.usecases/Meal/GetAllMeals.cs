using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class GetAllMeals : IUseCase<IEnumerable<domain.Meal>> {

        private readonly IMealRepository mealRepository;
        
        public GetAllMeals(IMealRepository mealRepository) {
            this.mealRepository = mealRepository;
        }


        public IEnumerable<domain.Meal> Execute() {
            return mealRepository.GetAll();
        }

    }

}