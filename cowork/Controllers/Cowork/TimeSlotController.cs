using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.TimeSlot;
using cowork.usecases.TimeSlot.Models;
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
            var res = new GetAllTimeSlots(Repository).Execute();
            return Ok(res);
        }


        [HttpGet("OfPlace/{placeId}")]
        public IActionResult AllOfPlace(long placeId) {
            var res = new GetTimeSlotsOfPlace(Repository, placeId).Execute();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateTimeSlotInput timeSlot) {
            var res = new CreateTimeSlot(Repository, timeSlot).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] TimeSlot timeSlot) {
            var res = new UpdateTimeSlot(Repository, timeSlot).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteTimeSlot(Repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetTimeSlotById(Repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetTimeSlotsWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}