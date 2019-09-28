using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using cowork.Controllers.RequestArguments;
using cowork.domain;
using cowork.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        public ILoginRepository LoginRepository;

        public IUserRepository Repository;
        public ISubscriptionRepository SubscriptionRepository;
        public AuthTokenHandler AuthTokenHandler;

        public UserController(IUserRepository repository, ILoginRepository loginRepository,
                              ISubscriptionRepository subscriptionRepository, AuthTokenHandler authTokenHandler) {
            Repository = repository;
            LoginRepository = loginRepository;
            SubscriptionRepository = subscriptionRepository;
            AuthTokenHandler = authTokenHandler;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            return Ok(Repository.GetById(id));
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration) {
            var result = Repository.Create(userRegistration.User);
            if (result == -1) return Conflict();
            userRegistration.User.Id = result;
            PasswordHashing.CreatePasswordHash(userRegistration.Password, out var hash, out var salt);
            result = LoginRepository.Create(new Login(-1, hash, salt, userRegistration.Email, result));
            if (result > -1) return Ok(userRegistration.User);
            Repository.DeleteById(userRegistration.User.Id);
            return Conflict();
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
        public IActionResult Auth([FromBody] Credentials credentials) {
            var userId = LoginRepository.Auth(credentials.Email, credentials.Password);
            if (userId == -1) return NotFound();
            var user = Repository.GetById(userId);
            if (user == null) return NotFound();
            var sub = SubscriptionRepository.GetOfUser(user.Id);
            var authToken = AuthTokenHandler.EncryptToken(new List<Claim> {
                new Claim("Role", user.Type.ToString()),
                new Claim("Id", user.Id.ToString())
            });
            if (sub != null && sub.FixedContract && sub.LatestRenewal.AddMonths(sub.Type.FixedContractDurationMonth) < DateTime.Today) {
                SubscriptionRepository.Delete(sub.Id);
                sub = null;
            } 
            return Ok(new {user, sub, auth_token = authToken});
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