using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class DeletePlace : IUseCase<bool> {

        private readonly IPlaceRepository placeRepository;
        public readonly long Id;

        public DeletePlace(IPlaceRepository placeRepository, long id) {
            this.placeRepository = placeRepository;
            Id = id;
        }


        public bool Execute() {
            return placeRepository.Delete(Id);
        }

    }

}