using cowork.Controllers.RequestArguments;
using coworkdomain;
using coworkpersistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {

        private ILoginRepository repository;


        public LoginController(ILoginRepository loginRepository) {
            repository = loginRepository;
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = repository.WithPaging(page, amount);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] LoginClearPassword login) {
            if (isEmailInvalid(login) || isPasswordInvalid(login))
                return BadRequest();
            var newLogin = CreateLoginModel(login);
            var res = repository.Create(newLogin);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] LoginClearPassword login) {
            if (isEmailInvalid(login))
                return BadRequest();
            Login newLogin;
            if (login.Password == "") {
                var current = repository.ById(login.Id);
                if (current == null) return NotFound();
                newLogin = new Login(login.Id, current.PasswordHash, current.PasswordSalt, login.Email, login.UserId);
            } else newLogin = CreateLoginModel(login);
            var res = repository.Update(newLogin);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        private bool isEmailInvalid(LoginClearPassword login) {
            return string.IsNullOrEmpty(login.Email) || string.IsNullOrWhiteSpace(login.Email);
        }


        private bool isPasswordInvalid(LoginClearPassword login) {
            return string.IsNullOrEmpty(login.Password) || string.IsNullOrWhiteSpace(login.Password);
        }


        private static Login CreateLoginModel(LoginClearPassword login) {
            PasswordHashing.CreatePasswordHash(login.Password, out var hash, out var salt);
            var newLogin = new Login(login.Id, hash, salt, login.Email, login.UserId);
            return newLogin;
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var res = repository.Delete(id);
            if(!res) return NotFound();
            return Ok();
        }
    }

}