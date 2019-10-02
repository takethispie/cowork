using System.Collections;
using System.Collections.Generic;
using cowork.domain.Interfaces;

namespace cowork.usecases.Ware {

    public class GetWaresWithPaging : IUseCase<IEnumerable<domain.Ware>> {

        private readonly IWareRepository wareRepository;
        public readonly int Page;
        public readonly int Amount;

        public GetWaresWithPaging(IWareRepository wareRepository, int page, int amount) {
            this.wareRepository = wareRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<domain.Ware> Execute() {
            return wareRepository.GetAllWithPaging(Page, Amount);
        }

    }

}