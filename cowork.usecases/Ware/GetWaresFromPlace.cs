using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class GetWaresFromPlace : IUseCase<IEnumerable<domain.Ware>> {

        private readonly IWareRepository wareRepository;
        public readonly long PlaceId;

        public GetWaresFromPlace(IWareRepository wareRepository, long placeId) {
            this.wareRepository = wareRepository;
            PlaceId = placeId;
        }


        public IEnumerable<domain.Ware> Execute() {
            return wareRepository.GetAllFromPlace(PlaceId);
        }

    }

}