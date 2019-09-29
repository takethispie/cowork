using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        private readonly ILoginRepository loginRepository;
        public IUserRepository Repository;
        public ISubscriptionRepository SubscriptionRepository;
        public AuthTokenHandler AuthTokenHandler;

        public UserController(IUserRepository repository, ILoginRepository loginRepository,
                              ISubscriptionRepository subscriptionRepository, AuthTokenHandler authTokenHandler) {
            Repository = repository;
            this.loginRepository = loginRepository;
            SubscriptionRepository = subscriptionRepository;
            AuthTokenHandler = authTokenHandler;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            return Ok(Repository.GetById(id));
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationInput userRegistrationInput) {
            return Ok(new RegisterAuth(loginRepository, Repository, userRegistrationInput).Execute());
        }

        
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] User user) {
            if (!User.Claims.Any(claim => claim.Type == "Role" && claim.Value == UserType.Admin.ToString()))
                return Unauthorized();
            var result = Repository.Create(user);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] User user) {
            var result = Repository.Update(user);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.DeleteById(id);
            if (!result) return Conflict();
            return Ok();
        }


        [HttpPost("auth")]
        public IActionResult Auth([FromBody] CredentialsInput credentialsInput) {
            var cmd = new AuthUser(loginRepository, Repository, SubscriptionRepository, AuthTokenHandler,
                credentialsInput);
            return Ok(cmd.Execute());
        }


        [Authorize]
        [HttpGet("all")]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }


        [Authorize]
        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}