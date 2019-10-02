using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class GetWareById : IUseCase<domain.Ware> {

        private readonly IWareRepository wareRepository;
        public readonly long Id;

        public GetWareById(IWareRepository wareRepository, long id) {
            this.wareRepository = wareRepository;
            Id = id;
        }


        public domain.Ware Execute() {
            return wareRepository.GetById(Id);
        }

    }

}