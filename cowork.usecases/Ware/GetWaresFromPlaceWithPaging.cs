using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class GetWaresFromPlaceWithPaging : IUseCase<IEnumerable<domain.Ware>> {

        private readonly IWareRepository wareRepository;
        public readonly long PlaceId;
        public readonly int Page;
        public readonly int Amount;

        public GetWaresFromPlaceWithPaging(IWareRepository wareRepository, long placeId, int page, int amount) {
            this.wareRepository = wareRepository;
            PlaceId = placeId;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Ware> Execute() {
            return wareRepository.GetAllFromPlaceWithPaging(PlaceId, Amount, Page);
        }

    }

}