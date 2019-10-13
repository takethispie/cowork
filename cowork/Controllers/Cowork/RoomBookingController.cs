using System;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.RoomBooking;
using cowork.usecases.RoomBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class RoomBookingController : ControllerBase {

        private readonly IRoomBookingRepository repository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IRoomRepository roomRepository;


        public RoomBookingController(IRoomBookingRepository repository, ITimeSlotRepository timeSlotRepository, 
                                     IRoomRepository roomRepository) {
            this.timeSlotRepository = timeSlotRepository;
            this.roomRepository = roomRepository;
            this.repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = new GetAllRoomBookings(repository).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateRoomBookingInput roomBooking) {
            var result = new CreateRoomBooking(repository, timeSlotRepository, roomRepository, roomBooking).Execute();
            if (result == -1) return BadRequest("Impossible de créer la réservation");
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] RoomBooking roomBooking) {
            var result = new UpdateRoomBooking(repository, roomRepository, timeSlotRepository, roomBooking).Execute();
            if (result == -1) return BadRequest("Impossible de mettre à jour la réservation");
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteRoombooking(repository, id).Execute();
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult AllOfUser(long userId) {
            var result = new GetRoomBookingsOfUser(repository, userId).Execute();
            return Ok(result);
        }


        [HttpGet("OfRoom/{roomId}")]
        public IActionResult AllOfRoom(long roomId) {
            var result = new GetBookingsOfRoom(repository, roomId).Execute();
            return Ok(result);
        }


        [HttpPost("FromGivenDate")]
        public IActionResult AllFromGivenDate([FromBody] DateTime dateTime) {
            var result = new GetRoomBookingsFromDate(repository, dateTime).Execute();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetRoomBookingById(repository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost("GetAllOfRoomStartingAtDate/{roomId}")]
        public IActionResult AllOfRoomStartingAtDate(long roomId, [FromBody] DateTime dateTime) {
            var result = new GetBookingsOfRoomStartingAt(repository, roomId, dateTime).Execute();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetRoomBookingsWithPaging(repository, page, amount).Execute();
            return Ok(result);
        }

    }

}