using System;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class GetMealBookingsFromDateAndPlace : IUseCase<IEnumerable<domain.MealBooking>> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly DateTime DateTime;
        public readonly long PlaceId;

        public GetMealBookingsFromDateAndPlace(IMealBookingRepository mealBookingRepository, DateTime dateTime, long placeId) {
            this.mealBookingRepository = mealBookingRepository;
            DateTime = dateTime;
            PlaceId = placeId;
        }


        public IEnumerable<domain.MealBooking> Execute() {
            return mealBookingRepository.GetAllFromDateAndPlace(DateTime, PlaceId);
        }

    }

}