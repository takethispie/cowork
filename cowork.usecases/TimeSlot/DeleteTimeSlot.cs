using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class DeleteTimeSlot : IUseCase<bool> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long Id;

        public DeleteTimeSlot(ITimeSlotRepository timeSlotRepository, long id) {
            this.timeSlotRepository = timeSlotRepository;
            Id = id;
        }


        public bool Execute() {
            return timeSlotRepository.Delete(Id);
        }

    }

}