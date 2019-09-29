using System.Collections.Generic;
using System.Linq;
using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class GetAllPlaces : IUseCase<IEnumerable<domain.Place>> {

        private readonly IPlaceRepository placeRepository;
        private readonly ITimeSlotRepository timeSlotRepository;

        public GetAllPlaces(IPlaceRepository placeRepository, ITimeSlotRepository timeSlotRepository) {
            this.placeRepository = placeRepository;
            this.timeSlotRepository = timeSlotRepository;
        }


        public IEnumerable<domain.Place> Execute() {
            return placeRepository.GetAll().Select(place => {
                place.OpenedTimes = timeSlotRepository.GetAllOfPlace(place.Id);
                return place;
            }).ToList();
        }

    }

}