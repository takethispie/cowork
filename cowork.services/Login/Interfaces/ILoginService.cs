using System.Collections;
using System.Collections.Generic;
using cowork.domain;
using cowork.services.Login.Models;

namespace cowork.services.Login {

    public interface ILoginService {

        long Create(LoginInput login);
        long Update(LoginInput login);
        void Delete(long id);
        IEnumerable<domain.Login> WithPaging(int page, int amount);
        UserRegistrationOutput Register(UserRegistrationInput userRegistrationInput);
        AuthOutput Auth(CredentialsInput credentialsInput);

    }

}