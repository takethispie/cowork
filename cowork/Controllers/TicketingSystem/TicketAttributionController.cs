using System.Linq;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.TicketingSystem {

    [Authorize]
    [Route("api/[controller]")]
    public class TicketAttributionController : ControllerBase {

        private ITicketAttributionRepository repository;
        private IUserRepository userRepository;
        private ITicketRepository ticketRepository;
        private ITicketCommentRepository ticketCommentRepository;

        public TicketAttributionController(ITicketAttributionRepository repository, IUserRepository userRepository, 
                                           ITicketRepository ticketRepository, ITicketCommentRepository ticketCommentRepository) {
            this.repository = repository;
            this.userRepository = userRepository;
            this.ticketRepository = ticketRepository;
            this.ticketCommentRepository = ticketCommentRepository;
        }

        [HttpGet]
        public IActionResult AllAttribution() {
            var result = repository.GetAll();
            return Ok(result);
        }
        
        
        [HttpGet("AttributedTo/{personnalId}")]
        public IActionResult AllAttributedTo(long personnalId) {
            var personnal = userRepository.GetById(personnalId);
            if (personnal == null) return NotFound("Personnel introuvable");
            var res = repository.GetAllFromStaffId(personnalId)
                .Select(ticketAttr => {
                    var ticket = ticketRepository.GetById(ticketAttr.TicketId);
                    ticket.AttributedTo = userRepository.GetById(ticketAttr.StaffId);
                    return ticket;
                })
                .Select(ticket => {
                    ticket.Comments =
                        ticketCommentRepository.GetByTicketId(ticket.Id);
                    return ticket;
                });
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TicketAttribution ticketAttribution) {
            var result = repository.Create(ticketAttribution);
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