using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class GetMealBookingsWithPaging : IUseCase<IEnumerable<domain.MealBooking>> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetMealBookingsWithPaging(IMealBookingRepository mealBookingRepository, int page, int amount) {
            this.mealBookingRepository = mealBookingRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.MealBooking> Execute() {
            return mealBookingRepository.GetAllWithPaging(Page, Amount);
        }

    }

}