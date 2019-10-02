using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Place;
using cowork.usecases.Place.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase {

        public IPlaceRepository Repository;
        public ITimeSlotRepository TimeSlotRepository;


        public PlaceController(IPlaceRepository repository, ITimeSlotRepository timeSlotRepository) {
            Repository = repository;
            TimeSlotRepository = timeSlotRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = new GetAllPlaces(Repository, TimeSlotRepository).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreatePlaceInput place) {
            var result = new CreatePlace(Repository, place).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Place place) {
            var result = new UpdatePlace(Repository, place).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("ById/{id}")]
        public IActionResult Delete(long id) {
            var result = new DeletePlace(Repository, id).Execute();
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = new GetPlaceById(Repository, TimeSlotRepository, id).Execute();
            return Ok(result);
        }


        [HttpGet("ByName/{name}")]
        public IActionResult ByName(string name) {
            var result = new GetPlaceByName(Repository, TimeSlotRepository, name).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetPlacesWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}