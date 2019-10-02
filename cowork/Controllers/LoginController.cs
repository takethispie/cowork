using cowork.domain.Interfaces;
using cowork.usecases.Auth;
using cowork.usecases.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers {

    [Authorize]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {

        private readonly ILoginRepository loginRepository;


        public LoginController(ILoginRepository loginRepository) {
            this.loginRepository = loginRepository;
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            return Ok(new GetAllAuthWithPaging(loginRepository, page, amount).Execute());
        }


        [HttpPost]
        public IActionResult Create([FromBody] LoginInput login) {
            return Ok(new CreateAuth(loginRepository, login).Execute());
        }


        [HttpPut]
        public IActionResult Update([FromBody] LoginInput login) {
            return Ok(new UpdateAuth(loginRepository, login).Execute());
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var res = new DeleteAuth(loginRepository, id).Execute();
            if (!res) return BadRequest("impossible de supprimer le login");
            return Ok();
        }
    }

}