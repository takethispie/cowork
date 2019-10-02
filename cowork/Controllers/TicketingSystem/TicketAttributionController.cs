using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.TicketAttribution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.TicketingSystem {

    [Authorize]
    [Route("api/[controller]")]
    public class TicketAttributionController : ControllerBase {

        private ITicketAttributionRepository repository;

        public TicketAttributionController(ITicketAttributionRepository repository) {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult AllAttribution() {
            var result = new GetAllTicketAttributions(repository).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TicketAttribution ticketAttribution) {
            var result = new CreateTicketAttribution(repository, ticketAttribution).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteTicketAttribution(repository, id).Execute();
            if (result == false) return NotFound();
            return Ok();
        }
        

        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = new GetTicketAttributionsWithPaging(repository, page, amount).Execute();
            return Ok(result);
        }

    }

}