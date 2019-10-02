using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.SubscriptionType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionTypeController : ControllerBase {

        public ISubscriptionTypeRepository Repository;


        public SubscriptionTypeController(ISubscriptionTypeRepository repository) {
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = new GetAllSubscriptionTypes(Repository).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionType sub) {
            var res = new CreateSubscriptionType(Repository, sub).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] SubscriptionType sub) {
            var res = new UpdateSubscriptionType(Repository, sub).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteSubscriptionType(Repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = new GetSubscriptionTypeById(Repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetSubscriptionTypesWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}