using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class GetAllTimeSlots : IUseCase<IEnumerable<domain.TimeSlot>> {

        private readonly ITimeSlotRepository timeSlotRepository;

        public GetAllTimeSlots(ITimeSlotRepository timeSlotRepository) {
            this.timeSlotRepository = timeSlotRepository;
        }


        public IEnumerable<domain.TimeSlot> Execute() {
            return timeSlotRepository.GetAll();
        }

    }

}