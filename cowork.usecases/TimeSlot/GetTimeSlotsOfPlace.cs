using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.TimeSlot {

    public class GetTimeSlotsOfPlace : IUseCase<IEnumerable<domain.TimeSlot>> {

        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long PlaceId;

        public GetTimeSlotsOfPlace(ITimeSlotRepository timeSlotRepository, long placeId) {
            this.timeSlotRepository = timeSlotRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.TimeSlot> Execute() {
            return timeSlotRepository.GetAllOfPlace(PlaceId);
        }

    }

}