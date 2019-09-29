using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class UpdatePlace : IUseCase<long> {

        private readonly IPlaceRepository placeRepository;
        public readonly domain.Place Place;

        public UpdatePlace(IPlaceRepository placeRepository, domain.Place place) {
            this.placeRepository = placeRepository;
            Place = place;
        }


        public long Execute() {
            return placeRepository.Create(Place);
        }

    }

}