using cowork.domain.Interfaces;
using cowork.usecases.MealBooking.Models;

namespace cowork.usecases.MealBooking {

    public class CreateMealBooking : IUseCase<long> {

        private readonly IMealBookingRepository mealBookingRepository;
        private readonly CreateMealBookingInput createMealBookingInput;

        public CreateMealBooking(IMealBookingRepository mealBookingRepository, CreateMealBookingInput mealBookingInput) {
            this.mealBookingRepository = mealBookingRepository;
            createMealBookingInput = mealBookingInput;
        }


        public long Execute() {
            var mealBooking = new domain.MealBooking(createMealBookingInput.MealId, createMealBookingInput.UserId,
                createMealBookingInput.Note);
            return mealBookingRepository.Create(mealBooking);
        }

    }

}