using System.Collections.Generic;
using System.Security.Claims;

namespace cowork.services.Login {

    public interface ITokenHandler {

        string EncryptToken(List<Claim> claims = null);

    }

}