using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class UpdateTimeSlot : IUseCase<long> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly domain.TimeSlot TimeSlot;

        public UpdateTimeSlot(ITimeSlotRepository timeSlotRepository, domain.TimeSlot timeSlot) {
            this.timeSlotRepository = timeSlotRepository;
            TimeSlot = timeSlot;
        }


        public long Execute() {
            return timeSlotRepository.Update(TimeSlot);
        }

    }

}