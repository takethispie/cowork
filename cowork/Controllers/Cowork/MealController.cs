using cowork.Controllers.RequestArguments;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Route("api/[controller]")]
    public class MealController : ControllerBase{

        public IMealRepository Repository;


        public MealController(IMealRepository repository) {
            Repository = repository;
        }


        [HttpGet()]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Meal meal) {
            var result = Repository.Create(meal);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return Conflict();
            return Ok(id);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Meal meal) {
            var result = Repository.Update(meal);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpGet("AllFromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var result = Repository.GetAllFromPlace(placeId);
            return Ok(result);
        }


        [HttpPost("FromPlaceAndDate")]
        public IActionResult FromPlaceAndDate([FromBody] MealArgument mealArgument) {
            var result = Repository.GetAllFromDateAndPlace(mealArgument.Date, mealArgument.PlaceId);
            return Ok(result);
        }
        
        [HttpPost("FromPlaceAndStartingAtDate")]
        public IActionResult FromPlaceStartingAtDate([FromBody] MealArgument mealArgument) {
            var result = Repository.GetAllFromPlaceStartingAtDate(mealArgument.PlaceId, mealArgument.Date);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }

}