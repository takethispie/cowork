using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using cowork.usecases.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        private readonly ILoginRepository loginRepository;
        public IUserRepository Repository;
        public ISubscriptionRepository SubscriptionRepository;
        public AuthTokenHandler AuthTokenHandler;
        private readonly ISubscriptionTypeRepository subscriptionTypeRepository;


        public UserController(IUserRepository repository, ILoginRepository loginRepository,
                              ISubscriptionRepository subscriptionRepository, AuthTokenHandler authTokenHandler, 
                              ISubscriptionTypeRepository subscriptionTypeRepository) {
            Repository = repository;
            this.loginRepository = loginRepository;
            SubscriptionRepository = subscriptionRepository;
            AuthTokenHandler = authTokenHandler;
            this.subscriptionTypeRepository = subscriptionTypeRepository;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            return Ok(new GetUserById(Repository, id).Execute());
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
            var result = new CreateUser(Repository, user).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] User user) {
            var result = new UpdateUser(Repository, user).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteUser(Repository, id).Execute();
            if (!result) return Conflict();
            return Ok();
        }


        [HttpPost("auth")]
        public IActionResult Auth([FromBody] CredentialsInput credentialsInput) {
            var cmd = new AuthUser(loginRepository, Repository, SubscriptionRepository, AuthTokenHandler, 
                subscriptionTypeRepository, credentialsInput);
            return Ok(cmd.Execute());
        }


        [Authorize]
        [HttpGet("all")]
        public IActionResult All() {
            var result = new GetAllUsers(Repository).Execute();
            return Ok(result);
        }


        [Authorize]
        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetUsersWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}