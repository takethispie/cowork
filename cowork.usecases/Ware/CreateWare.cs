using cowork.domain.Interfaces;
using cowork.usecases.Ware.Models;

namespace cowork.usecases.Ware {

    public class CreateWare : IUseCase<long> {

        private readonly IWareRepository wareRepository;
        public readonly CreateWareInput Input;

        public CreateWare(IWareRepository wareRepository, CreateWareInput input) {
            this.wareRepository = wareRepository;
            Input = input;
        }


        public long Execute() {
            var ware = new domain.Ware(Input.Name, Input.Description, Input.SerialNumber, Input.PlaceId,
                Input.InStorage);
            return wareRepository.Create(ware);
        }

    }

}