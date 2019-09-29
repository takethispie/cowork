using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Room;
using cowork.usecases.Room.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase {

        public IRoomRepository Repository;


        public RoomController(IRoomRepository repository) {
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var res = new GetAllRooms(Repository).Execute();
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateRoomInput room) {
            var res = new CreateRoom(Repository, room).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Room room) {
            var res = new UpdateRoom(Repository, room).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteRoom(Repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("FromPlace/{placeId}")]
        public IActionResult AllFromPlace(long placeId) {
            var res = new GetRoomsFromPlace(Repository, placeId).Execute();
            return Ok(res);
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = new GetRoomById(Repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetRoomsWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}