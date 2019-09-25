using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
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
            var result = Repository.GetAll().Select(place => {
                place.OpenedTimes = TimeSlotRepository.GetAllOfPlace(place.Id);
                return place;
            });
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Place place) {
            var result = Repository.Create(place);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Place place) {
            var result = Repository.Update(place);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("ById/{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            result.OpenedTimes = TimeSlotRepository.GetAllOfPlace(result.Id);
            return Ok(result);
        }


        [HttpGet("ByName/{name}")]
        public IActionResult ByName(string name) {
            var result = Repository.GetByName(name);
            if (result == null) return NotFound();
            result.OpenedTimes = TimeSlotRepository.GetAllOfPlace(result.Id);
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}