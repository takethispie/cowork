using cowork.domain.Interfaces;
using cowork.usecases.TimeSlot.Models;

namespace cowork.usecases.TimeSlot {

    public class CreateTimeSlot : IUseCase<long> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly CreateTimeSlotInput Input;

        public CreateTimeSlot(ITimeSlotRepository timeSlotRepository, CreateTimeSlotInput input) {
            this.timeSlotRepository = timeSlotRepository;
            Input = input;
        }


        public long Execute() {
            var ts = new domain.TimeSlot(Input.Day, Input.StartHour, Input.StartMinutes, Input.EndHour, 
                Input.EndMinutes, Input.PlaceId);
            return timeSlotRepository.Create(ts);
        }

    }

}