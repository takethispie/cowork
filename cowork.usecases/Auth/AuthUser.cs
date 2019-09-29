using System;
using System.Collections.Generic;
using System.Security.Claims;
using cowork.domain.Interfaces;
using cowork.services.Login;
using cowork.usecases.Auth.Models;

namespace cowork.usecases.Auth {

    public class AuthUser : IUseCase<AuthOutput> {

        private ILoginRepository loginRepository;
        private IUserRepository userRepository;
        private ISubscriptionRepository subscriptionRepository;
        private ITokenHandler tokenHandler;
        public readonly CredentialsInput CredentialsInput;


        public AuthUser(ILoginRepository loginRepository, IUserRepository userRepository, 
                        ISubscriptionRepository subscriptionRepository, ITokenHandler tokenHandler,
                        CredentialsInput credentialsInput) {
            this.loginRepository = loginRepository;
            this.userRepository = userRepository;
            this.subscriptionRepository = subscriptionRepository;
            this.tokenHandler = tokenHandler;
            CredentialsInput = credentialsInput;
        }


        public AuthOutput Execute() {
            var userId = loginRepository.Auth(CredentialsInput.Email, CredentialsInput.Password);
            if (userId == -1) throw new Exception("account not found");
            var user = userRepository.GetById(userId);
            if (user == null) throw new Exception("user not found");
            var sub = subscriptionRepository.GetOfUser(user.Id);
            var authToken = tokenHandler.EncryptToken(new List<Claim> {
                new Claim("Role", user.Type.ToString()),
                new Claim("Id", user.Id.ToString())
            });
            if (sub != null && sub.FixedContract && sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth) < DateTime.Today) {
                subscriptionRepository.Delete(sub.Id);
                sub = null;
            } 
            return new AuthOutput{user = user,sub = sub, auth_token = authToken};
        }

    }

}