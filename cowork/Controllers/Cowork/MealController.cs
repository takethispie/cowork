using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Meal;
using cowork.usecases.Meal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class MealController : ControllerBase {

        public IMealRepository Repository;


        public MealController(IMealRepository repository) {
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            return Ok(new GetAllMeals(Repository).Execute());
        }


        [HttpPost]
        public IActionResult Create([FromBody] MealInput meal) {
            var result = new CreateMeal(Repository, meal).Execute();
            if (result == -1) return BadRequest("Impossible de creer le repas");
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteMeal(Repository, id).Execute();
            if (!result) return BadRequest("Impossible de supprimer le repas");
            return Ok(id);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Meal meal) {
            var result = new UpdateMeal(Repository, meal).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpGet("AllFromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var result = new GetAllMealsFromPlace(Repository, placeId).Execute();
            return Ok(result);
        }


        [HttpPost("FromPlaceAndDate")]
        public IActionResult FromPlaceAndDate([FromBody] MealFilterInput mealFilterInput) {
            var result = new GetAllMealFromDateAndPlace(Repository, mealFilterInput.PlaceId, mealFilterInput.Date)
                .Execute();
            return Ok(result);
        }


        [HttpPost("FromPlaceAndStartingAtDate")]
        public IActionResult FromPlaceStartingAtDate([FromBody] MealFilterInput mealFilterInput) {
            var result = new GetAllMealsFromPlaceStartingAtDate(Repository, mealFilterInput.PlaceId, mealFilterInput.Date)
                .Execute();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetMealById(Repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = new GetMealsWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}