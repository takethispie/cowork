using System;

namespace cowork.domain {

    public class Login {
        
        public long Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }

        public Login() { }


        public Login(long id, byte[] passwordHash, byte[] passwordSalt, string email, long userId) {
            Id = id;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Email = email;
            UserId = userId;
        }


        public Login(string password, string email, long userId) {
            if(isEmailInvalid(email)) throw new Exception("Email invalide");
            if(isPasswordInvalid(password)) throw new Exception("mot de passe invalide");
            PasswordHashing.CreatePasswordHash(password, out var hash, out var salt);
            PasswordHash = hash;
            PasswordSalt = salt;
            UserId = userId;
            Email = email;
        }
        
        public Login(long id, string password, string email, long userId) {
            if(isEmailInvalid(email)) throw new Exception("Email invalide");
            if(isPasswordInvalid(password)) throw new Exception("mot de passe invalide");
            PasswordHashing.CreatePasswordHash(password, out var hash, out var salt);
            PasswordHash = hash;
            PasswordSalt = salt;
            UserId = userId;
            Id = id;
            Email = email;
        }

        private bool isEmailInvalid(string email) {
            return string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email);
        }


        private bool isPasswordInvalid(string password) {
            return string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password);
        }
    }

}