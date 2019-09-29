using System;
using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class GetPlaceById : IUseCase<domain.Place> {

        private readonly IPlaceRepository placeRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly long Id;

        public GetPlaceById(IPlaceRepository placeRepository, ITimeSlotRepository timeSlotRepository, long id) {
            this.placeRepository = placeRepository;
            this.timeSlotRepository = timeSlotRepository;
            Id = id;
        }


        public domain.Place Execute() {
            var place = placeRepository.GetById(Id);
            if (place == null) return null;
            place.OpenedTimes = timeSlotRepository.GetAllOfPlace(place.Id);
            return place;
        }

    }

}