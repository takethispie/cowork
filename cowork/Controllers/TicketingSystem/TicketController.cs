using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using cowork.usecases.Ticket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.TicketingSystem {

    [Authorize]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase {

        private readonly ITicketRepository repository;
        private readonly ITicketAttributionRepository ticketAttributionRepository;
        private readonly ITicketCommentRepository ticketCommentRepository;
        private readonly ITicketWareRepository ticketWareRepository;
        private readonly IUserRepository userRepository;


        public TicketController(ITicketRepository ticketRepository, IUserRepository userRepository,
                                ITicketAttributionRepository ticketAttributionRepository,
                                ITicketCommentRepository ticketCommentRepository, ITicketWareRepository ticketWareRepository) {
            repository = ticketRepository;
            this.userRepository = userRepository;
            this.ticketAttributionRepository = ticketAttributionRepository;
            this.ticketCommentRepository = ticketCommentRepository;
            this.ticketWareRepository = ticketWareRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var res = new GetAllTickets(userRepository, ticketAttributionRepository, repository,
                ticketCommentRepository).Execute();
            return Ok(res);
        }
        
        
        [HttpGet("AttributedTo/{personnalId}")]
        public IActionResult AllTicketsAttributedTo(long personnalId) {
            var res = new GetTicketsAttributedToUser(userRepository, ticketAttributionRepository, repository,
                ticketCommentRepository, personnalId).Execute();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateTicketInput createTicket) {
            var res = new CreateTicket(repository, createTicket).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] UpdateTicketInput createTicket) {
            var userIdClaim = User.Identities
                                  .FirstOrDefault(identity => identity.HasClaim(claim => claim.Type == "Id" || claim.Type == "Role"))
                                  ?.Claims.FirstOrDefault(claim => claim.Type == "Id");
            var succeedParsing = long.TryParse(userIdClaim?.Value, out var value);
            if (!succeedParsing) return Unauthorized();
            var res = new UpdateTicket(repository, ticketAttributionRepository, createTicket, value).Execute();
            if (res == -1) return BadRequest();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteTicket(repository, ticketCommentRepository, ticketAttributionRepository, 
                ticketWareRepository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetTicketById(repository, ticketAttributionRepository, userRepository, 
                ticketCommentRepository, id).Execute();
            return Ok(result);
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = new GetTicketsOfPlace(userRepository, ticketAttributionRepository, repository, 
                ticketCommentRepository, placeId).Execute();
            return Ok(res);
        }


        [HttpGet("OpenedBy/{userId}")]
        public IActionResult AllOpenedBy(long userId) {
            var res = new GetTicketsOpenedByUser(repository, userRepository, ticketAttributionRepository, 
                ticketCommentRepository, userId).Execute();
            return Ok(res);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = new GetTicketsWithPaging(repository, userRepository, ticketAttributionRepository,
                ticketCommentRepository, page, amount).Execute();
            return Ok(result);
        }


        [HttpGet("WithState/{state}")]
        public IActionResult AllWithState(int state) {
            var result = new GetTicketsWithState(repository, userRepository, ticketAttributionRepository,
                ticketCommentRepository, state).Execute();
            return Ok(result);
        }
    }

}