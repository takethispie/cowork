using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class DeleteWare : IUseCase<bool> {

        private readonly IWareRepository wareRepository;
        public readonly long Id;

        public DeleteWare(IWareRepository wareRepository, long id) {
            this.wareRepository = wareRepository;
            Id = id;
        }


        public bool Execute() {
            return wareRepository.Delete(Id);
        }

    }

}