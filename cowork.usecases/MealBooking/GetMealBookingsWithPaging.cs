using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.MealBooking {

    public class GetMealBookingsWithPaging : IUseCase<IEnumerable<domain.MealBooking>> {

        private readonly IMealBookingRepository mealBookingRepository;
        public readonly int Page;
        public readonly int Amount;
        public readonly bool ByDateDescending;

        public GetMealBookingsWithPaging(IMealBookingRepository mealBookingRepository, int page, int amount, bool byDateDescending) {
            this.mealBookingRepository = mealBookingRepository;
            Page = page;
            Amount = amount;
            ByDateDescending = byDateDescending;
        }


        public IEnumerable<domain.MealBooking> Execute() {
            return mealBookingRepository.GetAllWithPaging(Page, Amount, ByDateDescending);
        }

    }

}