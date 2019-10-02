namespace cowork.usecases.Auth.Models {

    public class LoginInput {

        public long Id { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }
        public string Password { get; set; }

    }

}