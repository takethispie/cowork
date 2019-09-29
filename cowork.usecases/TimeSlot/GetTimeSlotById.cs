using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class GetTimeSlotById : IUseCase<domain.TimeSlot> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long Id;
        
        public GetTimeSlotById(ITimeSlotRepository timeSlotRepository, long id) {
            this.timeSlotRepository = timeSlotRepository;
            Id = id;
        }


        public domain.TimeSlot Execute() {
            return timeSlotRepository.GetById(Id);
        }

    }

}