using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class SubscriptionTypeController : ControllerBase {

        public ISubscriptionTypeRepository Repository;


        public SubscriptionTypeController(ISubscriptionTypeRepository repository) {
            Repository = repository;
        }
        
        [HttpGet]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] SubscriptionType sub) {
            var res = Repository.Create(sub);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] SubscriptionType sub) {
            var res = Repository.Update(sub);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }
        
        
        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("ByName/{name}")]
        public IActionResult ByName(string name) {
            var result = Repository.GetByName(name);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }

}