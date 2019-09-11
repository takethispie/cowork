using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {
    
    [Authorize]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase {

        public ITimeSlotRepository Repository;


        public TimeSlotController(ITimeSlotRepository repository) {
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var res = Repository.GetAll();
            return Ok(res);
        }


        [HttpGet("OfPlace/{roomId}")]
        public IActionResult AllOfPlace(long roomId) {
            var res = Repository.GetAllOfPlace(roomId);
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TimeSlot timeSlot) {
            var res = Repository.Create(timeSlot);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] TimeSlot timeSlot) {
            var res = Repository.Update(timeSlot);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllByPaging(page, amount);
            return Ok(result);
        }

    }

}