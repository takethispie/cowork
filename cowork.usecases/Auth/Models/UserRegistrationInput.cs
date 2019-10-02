using cowork.domain;

namespace cowork.usecases.Auth.Models {

    public class UserRegistrationInput {

        public domain.User User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

}