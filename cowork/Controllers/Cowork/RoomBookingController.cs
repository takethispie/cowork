using System;
using System.Linq;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class RoomBookingController : ControllerBase {

        public IRoomBookingRepository Repository;

        public ITimeSlotRepository TimeSlotRepository;


        public RoomBookingController(IRoomBookingRepository repository, IPlaceRepository placeRepository,
                                     ITimeSlotRepository timeSlotRepository) {
            TimeSlotRepository = timeSlotRepository;
            Repository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = Repository.GetAll();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] RoomBooking roomBooking) {
            var placeId = roomBooking.Room.Place.Id;
            var canBook = TimeSlotRepository
                          .GetAllOfPlace(placeId)
                          .Any(slot => slot.Day == roomBooking.Start.DayOfWeek
                                       && new TimeSpan(0, slot.EndHour, slot.EndMinutes, 0)
                                       >= new TimeSpan(0, roomBooking.End.Hour, roomBooking.End.Minute, 0)
                                       && new TimeSpan(0, slot.StartHour, slot.StartMinutes, 0)
                                       <= new TimeSpan(0, roomBooking.Start.Hour, roomBooking.Start.Minute, 0));
            if (!canBook) return Conflict();
            var result = Repository.Create(roomBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] RoomBooking roomBooking) {
            var possibleConflicts = Repository.GetAllFromGivenDate(roomBooking.Start.Date)
                                              .Where(rb => rb.Id != roomBooking.Id).ToList();
            var hasNoConflict = possibleConflicts.All(booking =>
                booking.End <= roomBooking.Start || booking.Start >= roomBooking.End);
            if (!hasNoConflict) return Conflict("Une réservation est déjà présente pour ces horaires");
            var result = Repository.Update(roomBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return Conflict();
            return Ok();
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult AllOfUser(long userId) {
            var result = Repository.GetAllOfUser(userId);
            return Ok(result);
        }


        [HttpGet("OfRoom/{roomId}")]
        public IActionResult AllOfRoom(long roomId) {
            var result = Repository.GetAllOfRoom(roomId);
            return Ok(result);
        }


        [HttpPost("FromGivenDate")]
        public IActionResult AllFromGivenDate([FromBody] DateTime dateTime) {
            var result = Repository.GetAllFromGivenDate(dateTime);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost("GetAllOfRoomStartingAtDate/{roomId}")]
        public IActionResult AllOfRoomStartingAtDate(long roomId, [FromBody] DateTime dateTime) {
            var result = Repository.GetAllOfRoomStartingAtDate(roomId, dateTime);
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}