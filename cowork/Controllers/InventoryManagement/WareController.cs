using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Route("api/[controller]")]
    public class WareController : ControllerBase {

        private IWareRepository repository;


        public WareController(IWareRepository wareRepository) {
            repository = wareRepository;
        }
        
        [HttpGet]
        public IActionResult All() {
            var res = repository.GetAll();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Ware ware) {
            var res = repository.Create(ware);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Ware ware) {
            var res = repository.Update(ware);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }
        
        
        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = repository.GetById(id);
            if (result == null) return NotFound();
            return Ok();
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = repository.GetAllFromPlace(placeId);
            return Ok(res);
        }


        [HttpGet("FromPlaceWithPaging/{placeId}/{amount}/{page}")]
        public IActionResult AllFromPlaceWithPaging(long placeId, int amount, int page) {
            var res = repository.GetAllFromPlaceWithPaging(placeId, amount, page);
            return Ok(res);
        }

    }

}