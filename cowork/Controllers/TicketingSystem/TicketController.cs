using System.Linq;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
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
            var res = repository.GetAll().Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Ticket ticket) {
            var res = repository.Create(ticket);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Ticket ticket) {
            var userClaim = HttpContext.User;
            var userIdClaim = User.Identities
                                  .FirstOrDefault(identity => identity.HasClaim(claim => claim.Type == "Id" || claim.Type == "Role"))
                                  ?.Claims
                                  .FirstOrDefault(claim => claim.Type == "Id");
            if (ticket.AttributedTo == null 
                || (userIdClaim == null 
                || string.IsNullOrEmpty(userIdClaim.Value)
                || !long.TryParse(userIdClaim.Value, out var id)
                || ticket.AttributedTo.Id != id)) 
                return BadRequest("User can't modify status of a ticket not attributed to him");
            var res = repository.Update(ticket);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var comments = ticketCommentRepository.GetByTicketId(id);
            comments.ForEach(comment => ticketCommentRepository.Delete(comment.Id));
            var result = repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = repository.GetById(id);
            if (result == null) return NotFound();
            var ticketAttribution = ticketAttributionRepository.GetFromTicket(result.Id);
            if (ticketAttribution != null)
                result.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
            result.Comments = ticketCommentRepository.GetByTicketId(result.Id);
            return Ok();
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = repository.GetAllOfPlace(placeId).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return Ok(res);
        }


        [HttpGet("OpenedBy/{userId}")]
        public IActionResult AllOpenedBy(long userId) {
            var user = userRepository.GetById(userId);
            if (user == null) return NotFound("Utilisateur introuvable");
            var res = repository.GetAllOpenedBy(user).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return Ok(res);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = repository.GetAllByPaging(page, amount).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            return Ok(result);
        }


        [HttpGet("WithState/{state}")]
        public IActionResult AllWithState(int state) {
            var result = repository.GetAllWithState(state).Select(ticket => {
                var ticketAttribution = ticketAttributionRepository.GetFromTicket(ticket.Id);
                if (ticketAttribution != null)
                    ticket.AttributedTo = userRepository.GetById(ticketAttribution.StaffId);
                ticket.Comments = ticketCommentRepository.GetByTicketId(ticket.Id);
                return ticket;
            });
            
            return Ok(result);
        }
    }

}