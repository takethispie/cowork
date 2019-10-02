using System;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth.Models;

namespace cowork.usecases.Auth {

    public class RegisterAuth : IUseCase<UserRegistrationOutput> {

        private IUserRepository userRepository;
        private ILoginRepository loginRepository;
        public readonly UserRegistrationInput UserRegistrationInput;


        public RegisterAuth(ILoginRepository loginRepository, IUserRepository userRepository, 
                            UserRegistrationInput userRegistrationInput) {
            this.loginRepository = loginRepository;
            this.userRepository = userRepository;
            this.UserRegistrationInput = userRegistrationInput;
        }


        public UserRegistrationOutput Execute() {
            var result = userRepository.Create(UserRegistrationInput.User);
            if (result == -1) throw new Exception("Error adding new user in database");
            UserRegistrationInput.User.Id = result;
            var login = new Login(UserRegistrationInput.Password, UserRegistrationInput.Email, result);
            result = loginRepository.Create(login);
            if (result > -1) return new UserRegistrationOutput{ User = UserRegistrationInput.User};
            userRepository.DeleteById(UserRegistrationInput.User.Id);
            throw new Exception("Error registering new user");
        }

    }

}