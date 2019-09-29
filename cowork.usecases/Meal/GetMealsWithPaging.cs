using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class GetMealsWithPaging : IUseCase<IEnumerable<domain.Meal>> {

        private readonly IMealRepository mealRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetMealsWithPaging(IMealRepository mealRepository, int page, int amount) {
            this.mealRepository = mealRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Meal> Execute() {
            return mealRepository.GetAllByPaging(Page, Amount);
        }

    }

}