using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Meal.Models;
using cowork.usecases.MealBooking;
using cowork.usecases.MealBooking.Models;
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
        public IActionResult Create([FromBody] CreateMealBookingInput mealBooking) {
            var result = new CreateMealBooking(Repository, mealBooking).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] MealBooking mealBooking) {
            var result = new UpdateMealBooking(Repository, mealBooking).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteMealBooking(Repository, id).Execute();
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetMealBookinById(Repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("FromUser/{userId}")]
        public IActionResult AllFromUser(long userId) {
            var result = new GetMealBookingsFromUser(Repository, userId).Execute();
            return Ok(result);
        }


        [HttpPost("FromDateAndPlace")]
        public IActionResult AllFromDateAndPlace([FromBody] MealFilterInput mealFilterInput) {
            var result = new GetMealBookingsFromDateAndPlace(Repository, mealFilterInput.Date, mealFilterInput.PlaceId)
                .Execute();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = new GetMealBookingsWithPaging(Repository, page, amount, true).Execute();
            return Ok(result);
        }

    }

}