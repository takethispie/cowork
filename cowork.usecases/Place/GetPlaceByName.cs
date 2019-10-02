using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class GetPlaceByName : IUseCase<domain.Place> {

        private readonly IPlaceRepository placeRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        public readonly string Name;

        public GetPlaceByName(IPlaceRepository placeRepository, ITimeSlotRepository timeSlotRepository, string name) {
            this.placeRepository = placeRepository;
            this.timeSlotRepository = timeSlotRepository;
            Name = name;
        }


        public domain.Place Execute() {
            var place = placeRepository.GetByName(Name);
            if (place == null) return null;
            place.OpenedTimes = timeSlotRepository.GetAllOfPlace(place.Id);
            return place;
        }

    }

}