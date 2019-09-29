using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Auth {

    public class GetAllAuthWithPaging : IUseCase<IEnumerable<Login>> {

        private ILoginRepository loginRepository;
        public readonly int Page;
        public readonly int Amount;


        public GetAllAuthWithPaging(ILoginRepository loginRepository, int page, int amount) {
            this.loginRepository = loginRepository;
            Page = page;
            Amount = amount;
        }


        public IEnumerable<Login> Execute() {
            return loginRepository.WithPaging(Page, Amount);
        }

    }

}