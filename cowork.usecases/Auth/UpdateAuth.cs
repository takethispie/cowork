using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth.Models;

namespace cowork.usecases.Auth {

    public class UpdateAuth : IUseCase<long> {

        private ILoginRepository loginRepository;
        public LoginInput LoginInput;
        
        public UpdateAuth(ILoginRepository loginRepository, LoginInput loginInput) {
            this.loginRepository = loginRepository;
            LoginInput = loginInput;
        }
        

        public long Execute() {
            Login newLogin;
            if (LoginInput.Password == "") {
                var current = loginRepository.ById(LoginInput.Id);
                if (current == null) throw new Exception("not found");
                newLogin = new Login(LoginInput.Id, current.PasswordHash, current.PasswordSalt, LoginInput.Email, LoginInput.UserId);
            } else newLogin = new Login(LoginInput.Id, LoginInput.Password, LoginInput.Email, LoginInput.UserId);
            return loginRepository.Update(newLogin);
        }

    }

}