using cowork.domain.Interfaces;
using cowork.usecases.Place.Models;

namespace cowork.usecases.Place {

    public class CreatePlace : IUseCase<long> {

        private readonly IPlaceRepository placeRepository;
        public readonly CreatePlaceInput input;

        public CreatePlace(IPlaceRepository placeRepository, CreatePlaceInput placeInput) {
            this.placeRepository = placeRepository;
            input = placeInput;
        }


        public long Execute() {
            var place = new domain.Place(input.Name, input.HighBandwidthWifi, input.UnlimitedBeverages,
                input.MembersOnlyArea, input.CosyRoomAmount, input.PrinterAmount, input.LaptopAmount);
            return placeRepository.Create(place);
        }

    }

}