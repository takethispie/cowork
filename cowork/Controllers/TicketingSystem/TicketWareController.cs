using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.TicketingSystem {

    [Authorize]
    [Route("api/[controller]")]
    public class TicketWareController : ControllerBase{

        private ITicketWareRepository repository;
        
        public TicketWareController(ITicketWareRepository repository) {
            this.repository = repository;
        }


        [HttpGet]
        public IActionResult GetAll() {
            var result = repository.GetAll();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            var result = repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] TicketWare ticketWare) {
            var result = repository.Create(ticketWare);
            if (result == -1) return Conflict();
            return Ok(result);
        }
        
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = repository.Delete(id);
            if (result == false) return NotFound();
            return Ok();
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}