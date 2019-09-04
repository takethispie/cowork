using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Route("api/[controller]")]
    public class StaffLocationController : ControllerBase {

        private IStaffLocationRepository repository;


        public StaffLocationController(IStaffLocationRepository staffLocationRepository) {
            repository = staffLocationRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = repository.GetAll();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpPost]
        public IActionResult Create([FromBody] StaffLocation staffLocation) {
            var result = repository.Create(staffLocation);
            if (result == -1) return Conflict();
            return Ok(result);
        }

    }

}