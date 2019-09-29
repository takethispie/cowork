using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.TicketingSystem {
    [Authorize]
    [Route("api/[controller]")]
    public class TicketCommentController : ControllerBase {

        private readonly ITicketCommentRepository repository;
        
        public TicketCommentController(ITicketCommentRepository repository) {
            this.repository = repository;
        }


        [HttpGet]
        public IActionResult AllComments() {
            var result = new GetAllTicketComments(repository).Execute();
            return Ok(result);
        }
        
        
        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult CommentsWithPaging(int page, int amount) {
            var result = new GetTicketCommentsWithPaging(repository, page, amount).Execute();
            return Ok(result);
        }
        
        
        [HttpGet("LastCommentsFromUser/{userId}/{commentAmount}")]
        public IActionResult LastCommentsFromUser(long userId, int commentAmount) {
            var result = new GetLastCommentsFromUser(repository, commentAmount, userId).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TicketComment ticketComment) {
            if (ticketComment == null) return BadRequest();
            var result = new CreateTicketComment(repository, ticketComment).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{commentId}")]
        public IActionResult Delete(long commentId) {
            var result = new DeleteTicketComment(repository, commentId).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("Of/{ticketId}")]
        public IActionResult CommentsOf(long ticketId) {
            var result = new GetCommentsOfTicket(repository, ticketId).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }

    }

}