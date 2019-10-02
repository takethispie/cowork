using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.StaffLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Authorize]
    [Route("api/[controller]")]
    public class StaffLocationController : ControllerBase {

        private readonly IStaffLocationRepository repository;


        public StaffLocationController(IStaffLocationRepository staffLocationRepository) {
            repository = staffLocationRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = new GetAllStaffLocations(repository).Execute();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteStaffLocation(repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpPost]
        public IActionResult Create([FromBody] StaffLocation staffLocation) {
            var result = new CreateStaffLocation(repository, staffLocation).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetStaffLoationsWithPaging(repository, page, amount).Execute();
            return Ok(result);
        }

    }

}