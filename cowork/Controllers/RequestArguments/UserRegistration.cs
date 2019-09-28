using cowork.domain;

namespace cowork.Controllers.RequestArguments {

    public class UserRegistration {

        public User User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

}