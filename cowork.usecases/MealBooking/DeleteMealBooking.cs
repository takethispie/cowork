using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class DeleteMealBooking : IUseCase<bool> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly long Id;

        public DeleteMealBooking(IMealBookingRepository mealBookingRepository, long id) {
            this.mealBookingRepository = mealBookingRepository;
            Id = id;
        }


        public bool Execute() {
            return mealBookingRepository.Delete(Id);
        }

    }

}