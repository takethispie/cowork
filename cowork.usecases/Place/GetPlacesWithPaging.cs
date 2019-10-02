using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Place {

    public class GetPlacesWithPaging : IUseCase<IEnumerable<domain.Place>> {

        private readonly IPlaceRepository placeRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetPlacesWithPaging(IPlaceRepository placeRepository, int page, int amount) {
            this.placeRepository = placeRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Place> Execute() {
            return placeRepository.GetAllWithPaging(Page, Amount);
        }

    }

}