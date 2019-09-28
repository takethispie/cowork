using cowork.domain;
using cowork.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase {

        public IRoomRepository Repository;


        public RoomController(IRoomRepository repository) {
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var res = Repository.GetAll();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Room room) {
            var res = Repository.Create(room);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Room room) {
            var res = Repository.Update(room);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = Repository.GetAllFromPlace(placeId);
            return Ok(res);
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


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}