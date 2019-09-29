using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class UpdateMealBooking : IUseCase<long> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly domain.MealBooking MealBooking;

        public UpdateMealBooking(IMealBookingRepository mealBookingRepository, domain.MealBooking mealBooking) {
            this.mealBookingRepository = mealBookingRepository;
            MealBooking = mealBooking;
        }


        public long Execute() {
            return mealBookingRepository.Update(MealBooking);
        }

    }

}