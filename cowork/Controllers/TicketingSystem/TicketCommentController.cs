using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
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
            var result = repository.GetAll();
            return Ok(result);
        }
        
        
        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult CommentsWithPaging(int page, int amount) {
            var result = repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }
        
        
        [HttpGet("LastCommentsFromUser/{userId}/{commentAmount}")]
        public IActionResult LastCommentsFromUser(long userId, int commentAmount) {
            var result = repository.LastCommentsFromUser(userId, commentAmount);
            return Ok(result);
        }


        [HttpPost]
        public IActionResult AddComment([FromBody] TicketComment ticketComment) {
            if (ticketComment == null) return BadRequest();
            var result = repository.Create(ticketComment);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(long commentId) {
            var result = repository.Delete(commentId);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("Of/{ticketId}")]
        public IActionResult CommentsOf(long ticketId) {
            var result = repository.GetByTicketId(ticketId);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }

}