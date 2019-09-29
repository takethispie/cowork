using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class GetAllWares : IUseCase<IEnumerable<domain.Ware>> {

        private readonly IWareRepository wareRepository;

        public GetAllWares(IWareRepository wareRepository) {
            this.wareRepository = wareRepository;
        }


        public IEnumerable<domain.Ware> Execute() {
            return wareRepository.GetAll();
        }

    }

}