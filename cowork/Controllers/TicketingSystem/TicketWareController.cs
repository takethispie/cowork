using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.TicketWare;
using cowork.usecases.TicketWare.Models;
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
            var result = new GetAllTicketWares(repository).Execute();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            var result = new GetTicketWareById(repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateTicketWareInput ticketWare) {
            var result = new CreateTicketWare(repository, ticketWare).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }
        
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteTicketWare(repository, id).Execute();
            if (result == false) return NotFound();
            return Ok();
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = new GetTicketWaresWithPaging(repository, amount, page).Execute();
            return Ok(result);
        }

    }

}