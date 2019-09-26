using System;
using System.Linq;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement;
using coworkdomain.InventoryManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Authorize]
    [Route("api/[controller]")]
    public class WareBookingController : ControllerBase {

        private readonly IWareBookingRepository bookingRepository;


        public WareBookingController(IWareBookingRepository repository, ITimeSlotRepository timeSlotRepository) {
            bookingRepository = repository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = bookingRepository.GetAll();
            return Ok(result);
        }


        [HttpGet("ByWareId/{wareId}")]
        public IActionResult AllByWareId(long id) {
            var result = bookingRepository.GetAllByWareId(id);
            return Ok(result);
        }


        [HttpPost("ByWareIdStartingAt/{wareId}")]
        public IActionResult AllByWareIdStartingAt(long wareId, [FromBody] DateTime dateTime) {
            var result = bookingRepository.GetAllByWareIdStartingAt(wareId, dateTime);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = bookingRepository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] WareBooking wareBooking) {
            var existing = bookingRepository.GetStartingAt(wareBooking.Start.Date)
                .Where(booking => booking.WareId == wareBooking.WareId)
                .Any(slot => slot.End >= wareBooking.End && slot.Start <= wareBooking.Start);
            if (existing) return BadRequest("Erreur: créneau déjà pris");
            
            var result = bookingRepository.Create(wareBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] WareBooking wareBooking) {
            var result = bookingRepository.Update(wareBooking);
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = bookingRepository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult OfUser(long userId) {
            var result = bookingRepository.GetByUser(userId);
            return Ok(result);
        }


        [HttpPost("AllStartingAt")]
        public IActionResult AllStartingAt([FromBody] DateTime dateTime) {
            var result = bookingRepository.GetStartingAt(dateTime);
            return Ok(result);
        }


        [HttpPost("WithPaging/{page}/{amount}")]
        public IActionResult PagingWithStartDate(int page, int amount, [FromBody] DateTime start) {
            var result = bookingRepository.GetWithPaging(page, amount, start);
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = bookingRepository.GetWithPaging(page, amount);
            return Ok(result);
        }


        [HttpPost("FromGivenDate")]
        public IActionResult FromGivenDate([FromBody] DateTime start) {
            var result = bookingRepository.GetAllFromDate(start);
            return Ok(result);
        }

    }

}