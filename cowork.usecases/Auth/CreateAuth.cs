using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth.Models;

namespace cowork.usecases.Auth {

    public class CreateAuth : IUseCase<long> {

        private ILoginRepository loginRepository;
        public readonly LoginInput LoginInput;


        public CreateAuth(ILoginRepository loginRepository, LoginInput loginInput) {
            this.loginRepository = loginRepository;
            LoginInput = loginInput;
        }
        

        public long Execute() {
            var newLogin = new Login(LoginInput.Password, LoginInput.Email, LoginInput.UserId);
            return loginRepository.Create(newLogin);
        }

    }

}