using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class GetMealBookingsFromUser : IUseCase<IEnumerable<domain.MealBooking>> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly long UserId;

        public GetMealBookingsFromUser(IMealBookingRepository mealBookingRepository, long userId) {
            this.mealBookingRepository = mealBookingRepository;
            UserId = userId;
        }


        public IEnumerable<domain.MealBooking> Execute() {
            return mealBookingRepository.GetAllFromUser(UserId);
        }

    }

}