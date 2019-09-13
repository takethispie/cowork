using System.Linq;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

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


        [HttpGet("AttributedTo/{personnalId}")]
        public IActionResult AllAttributedTo(long personnalId) {
            var personnal = userRepository.GetById(personnalId);
            if (personnal == null) return NotFound("Personnel introuvable");
            var res = ticketAttributionRepository.GetAllFromStaffId(personnalId)
                                                 .Select(ticketAttr => repository.GetById(ticketAttr.TicketId))
                                                 .Select(ticket => {
                                                     ticket.Comments =
                                                         ticketCommentRepository.GetByTicketId(ticket.Id);
                                                     return ticket;
                                                 });
            return Ok(res);
        }


        [HttpGet("AllAttributions")]
        public IActionResult AllAttribution() {
            var result = ticketAttributionRepository.GetAll();
            return Ok(result);
        }


        [HttpGet("AllComments")]
        public IActionResult AllComments() {
            var result = ticketCommentRepository.GetAll();
            return Ok(result);
        }


        [HttpPost("AddComment")]
        public IActionResult AddComment([FromBody] TicketComment ticketComment) {
            if (ticketComment == null) return BadRequest();
            var result = ticketCommentRepository.Create(ticketComment);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("DeleteComment/{commentId}")]
        public IActionResult DeleteComment(long commentId) {
            var result = ticketCommentRepository.Delete(commentId);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("CommentsOf/{ticketId}")]
        public IActionResult CommentsOf(long ticketId) {
            var result = ticketCommentRepository.GetByTicketId(ticketId);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost("Attribution")]
        public IActionResult CreateAttribution([FromBody] TicketAttribution ticketAttribution) {
            var result = ticketAttributionRepository.Create(ticketAttribution);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("Attribution/{id}")]
        public IActionResult DeleteAttribution(long id) {
            var result = ticketAttributionRepository.Delete(id);
            if (result == false) return NotFound();
            return Ok();
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult WithPaging(int page, int amount) {
            var result = repository.GetAllByPaging(page, amount);
            return Ok(result);
        }


        [HttpGet("CommentsWithPaging/{page}/{amount}")]
        public IActionResult CommentsWithPaging(int page, int amount) {
            var result = ticketCommentRepository.GetAllWithPaging(page, amount);
            return Ok(result);
        }


        [HttpGet("AttributionsWithPaging/{page}/{amount}")]
        public IActionResult AttributionsWithPaging(int page, int amount) {
            var result = repository.GetAllByPaging(page, amount);
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


        [HttpGet("WareWithPaging/{page}/{amount}")]
        public IActionResult allWareWithPaging(int page, int amount) {
            var result = ticketWareRepository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}