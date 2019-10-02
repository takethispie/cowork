using System;
using cowork.domain;
using cowork.domain.Interfaces;

namespace cowork.usecases.Auth {

    public class CreateAdminAccount : IUseCase<int> {

        private ILoginRepository loginRepository;
        private IUserRepository userRepository;
        private domain.User user;
        public readonly string Email;
        public readonly string Password;

        public CreateAdminAccount(ILoginRepository loginRepository, IUserRepository userRepository, domain.User user, string email, string password) {
            this.loginRepository = loginRepository;
            this.userRepository = userRepository;
            this.user = user;
            Email = email;
            Password = password;
        }


        public int Execute() {
            if(user.Type != UserType.Admin) throw new Exception("l'utilisateur n'a pas le rang nécéssaire");
            var result = userRepository.Create(user);
            if (result == -1) return -1;
            user.Id = result;
            var login = new Login(Password, Email, result);
            result = loginRepository.Create(login);
            if (result > -1) return 0;
            userRepository.DeleteById(user.Id);
            return -1;
        }

    }

}