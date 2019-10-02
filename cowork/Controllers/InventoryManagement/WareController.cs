using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ware;
using cowork.usecases.Ware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Authorize]
    [Route("api/[controller]")]
    public class WareController : ControllerBase {

        private readonly IWareRepository repository;


        public WareController(IWareRepository wareRepository) {
            repository = wareRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var res = new GetAllWares(repository).Execute();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateWareInput ware) {
            var res = new CreateWare(repository, ware).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Ware ware) {
            var res = new UpdateWare(repository, ware).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteWare(repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetWareById(repository, id).Execute();
            if (result == null) return NotFound();
            return Ok();
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = new GetWaresFromPlace(repository, placeId).Execute();
            return Ok(res);
        }


        [HttpGet("FromPlaceWithPaging/{placeId}/{amount}/{page}")]
        public IActionResult AllFromPlaceWithPaging(long placeId, int amount, int page) {
            var res = new GetWaresFromPlaceWithPaging(repository, placeId, page, amount).Execute();
            return Ok(res);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetWaresWithPaging(repository, page, amount).Execute();
            return Ok(result);
        }

    }

}