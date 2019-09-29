using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class UpdateWare : IUseCase<long> {

        private readonly IWareRepository wareRepository;
        public readonly domain.Ware Ware;

        public UpdateWare(IWareRepository wareRepository, domain.Ware ware) {
            this.wareRepository = wareRepository;
            Ware = ware;
        }


        public long Execute() {
            return wareRepository.Update(Ware);
        }

    }

}