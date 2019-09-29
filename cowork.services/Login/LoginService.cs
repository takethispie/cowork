using System;
using System.Collections.Generic;
using System.Security.Claims;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.services.Login.Models;

namespace cowork.services.Login {

    public class LoginService : ILoginService{

        private readonly IUserRepository userRepository;
        private readonly ILoginRepository loginRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly ITokenHandler tokenHandler;


        public LoginService(ILoginRepository loginRepository, IUserRepository userRepository, 
                            ISubscriptionRepository subscriptionRepository, ITokenHandler tokenHandler) {
            this.userRepository = userRepository;
            this.loginRepository = loginRepository;
            this.subscriptionRepository = subscriptionRepository;
            this.tokenHandler = tokenHandler;
        }
        
    }

}