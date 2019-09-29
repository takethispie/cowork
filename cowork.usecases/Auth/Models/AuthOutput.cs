using cowork.domain;

namespace cowork.usecases.Auth.Models {

    public class AuthOutput {

        public User user { get; set; }
        public domain.Subscription sub { get; set; }
        public string auth_token { get; set; } 

    }

}