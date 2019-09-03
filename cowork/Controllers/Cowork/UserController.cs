using System;
using Bogus;
using cowork.Controllers.RequestArguments;
using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class UserController : ControllerBase {

        public IUserRepository Repository;
        public ILoginRepository LoginRepository;
        public ISubscriptionRepository SubscriptionRepository;


        public UserController(IUserRepository repository, ILoginRepository loginRepository,
            ISubscriptionRepository subscriptionRepository) 
        {
            Repository = repository;
            LoginRepository = loginRepository;
            this.SubscriptionRepository = subscriptionRepository;
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


        [HttpPost]
        public IActionResult Create([FromBody] User user) {
            var result = Repository.Create(user);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] User user) {
            var result = Repository.Update(user);
            if (result == -1) return Conflict();
            return Ok(result);
        }


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
            return Ok(new { user, sub});
        }


        [HttpGet("all")]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }

    }

}