using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class GetTimeSlotsWithPaging : IUseCase<IEnumerable<domain.TimeSlot>> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetTimeSlotsWithPaging(ITimeSlotRepository timeSlotRepository, int page, int amount) {
            this.timeSlotRepository = timeSlotRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.TimeSlot> Execute() {
            return timeSlotRepository.GetAllByPaging(Page, Amount);
        }

    }

}