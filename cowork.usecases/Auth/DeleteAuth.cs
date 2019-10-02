using System;
using cowork.domain.Interfaces;

namespace cowork.usecases.Auth {

    public class DeleteAuth : IUseCase<bool> {

        private ILoginRepository loginRepository;
        public readonly long Id;


        public DeleteAuth(ILoginRepository loginRepository, long id) {
            this.loginRepository = loginRepository;
            Id = id;
        }
        
        public bool Execute() {
            return loginRepository.Delete(Id);
        }

    }

}