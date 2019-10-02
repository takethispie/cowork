using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class GetAllMealsFromPlace : IUseCase<IEnumerable<domain.Meal>> {

        private readonly IMealRepository mealRepository;
        public readonly long PlaceId;

        public GetAllMealsFromPlace(IMealRepository mealRepository, long placeId) {
            this.mealRepository = mealRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.Meal> Execute() {
            return mealRepository.GetAllFromPlace(PlaceId);
        }

    }

}