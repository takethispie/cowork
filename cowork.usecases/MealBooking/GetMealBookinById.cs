using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class GetMealBookinById : IUseCase<domain.MealBooking> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly long Id;

        public GetMealBookinById(IMealBookingRepository mealBookingRepository, long id) {
            this.mealBookingRepository = mealBookingRepository;
            Id = id;
        }


        public domain.MealBooking Execute() {
            return mealBookingRepository.GetById(Id);
        }

    }

}