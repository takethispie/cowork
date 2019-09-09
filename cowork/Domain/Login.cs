namespace coworkdomain {

    public class Login {

        public Login() { }


        public Login(long id, byte[] passwordHash, byte[] passwordSalt, string email, long userId) {
            Id = id;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Email = email;
            UserId = userId;
        }


        public Login(long id, string email, long userId) {
            Id = id;
            Email = email;
            UserId = userId;
            PasswordHash = new byte[] { };
            PasswordSalt = new byte[] { };
        }


        public long Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }

    }

}