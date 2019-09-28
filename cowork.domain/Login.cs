namespace cowork.domain {

    public class Login {

        public Login() { }


        public Login(long id, byte[] passwordHash, byte[] passwordSalt, string email, long userId) {
            Id = id;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Email = email;
            UserId = userId;
        }


        public long Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }

    }

}