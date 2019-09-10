using cowork.Controllers.RequestArguments;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class MealBookingController : ControllerBase {

        public IMealBookingRepository Repository;


        public MealBookingController(IMealBookingRepository repository) {
            Repository = repository;
        }


        [HttpGet("all")]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] MealBooking mealBooking) {
            var result = Repository.Create(mealBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] MealBooking mealBooking) {
            var result = Repository.Update(mealBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("FromUser/{userId}")]
        public IActionResult AllFromUser(long userId) {
            var result = Repository.GetAllFromUser(userId);
            return Ok(result);
        }


        [HttpPost("FromDateAndPlace")]
        public IActionResult AllFromDateAndPlace([FromBody] MealArgument mealArgument) {
            var result = Repository.GetAllFromDateAndPlace(mealArgument.Date, mealArgument.PlaceId);
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount, true);
            return Ok(result);
        }

    }

}