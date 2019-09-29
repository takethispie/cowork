using System;
using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Meal {

    public class GetAllMealFromDateAndPlace : IUseCase<IEnumerable<domain.Meal>> {

        private readonly IMealRepository mealRepository;
        public readonly long PlaceId;
        public readonly DateTime DateTime;

        public GetAllMealFromDateAndPlace(IMealRepository mealRepository, long placeId, DateTime dateTime) {
            this.mealRepository = mealRepository;
            PlaceId = placeId;
            DateTime = dateTime;
        }


        public IEnumerable<domain.Meal> Execute() {
            return mealRepository.GetAllFromDateAndPlace(DateTime, PlaceId);
        }

    }

}