using System;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.WareBooking;
using cowork.usecases.WareBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.InventoryManagement {

    [Authorize]
    [Route("api/[controller]")]
    public class WareBookingController : ControllerBase {

        private readonly IWareBookingRepository bookingRepository;
        private readonly ITimeSlotRepository timeSlotRepository;
        private readonly IWareRepository wareRepository;


        public WareBookingController(IWareBookingRepository repository, ITimeSlotRepository timeSlotRepository, 
                                     IWareRepository wareRepository) {
            bookingRepository = repository;
            this.timeSlotRepository = timeSlotRepository;
            this.wareRepository = wareRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = new GetAllWareBookings(bookingRepository).Execute();
            return Ok(result);
        }


        [HttpGet("ByWareId/{wareId}")]
        public IActionResult AllByWareId(long id) {
            var result = new GetWareBookingsByWareId(bookingRepository, id).Execute();
            return Ok(result);
        }


        [HttpPost("ByWareIdStartingAt/{wareId}")]
        public IActionResult AllByWareIdStartingAt(long wareId, [FromBody] DateTime dateTime) {
            var result = new GetWareBookingsByWareIdStartingAt(bookingRepository, wareId, dateTime).Execute();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult ById(long id) {
            var result = new GetWareBookingById(bookingRepository, id).Execute();
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateWareBookingInput wareBooking) {
            var result = new CreateWareBooking(bookingRepository, timeSlotRepository, wareRepository,
                wareBooking).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpPut]
        public IActionResult Update([FromBody] WareBooking wareBooking) {
            var result = new UpdateWareBooking(bookingRepository, wareBooking).Execute();
            if (result == -1) return Conflict();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteWareBooking(bookingRepository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult OfUser(long userId) {
            var result = new GetWareBookinsOfUser(bookingRepository, userId).Execute();
            return Ok(result);
        }


        [HttpPost("AllStartingAt")]
        public IActionResult AllStartingAt([FromBody] DateTime dateTime) {
            var result = new GetWareBookingsStartingAtDate(bookingRepository, dateTime).Execute();
            return Ok(result);
        }


        [HttpPost("WithPaging/{page}/{amount}")]
        public IActionResult PagingWithStartDate(int page, int amount, [FromBody] DateTime start) {
            var result = new GetWareBookingsWithPaging(bookingRepository, page, amount, start).Execute();
            return Ok(result);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetWareBookingsWithPaging(bookingRepository, page, amount, null).Execute();
            return Ok(result);
        }


        [HttpPost("FromGivenDate")]
        public IActionResult FromGivenDate([FromBody] DateTime start) {
            var result = new GetWareBookingsFromDate(bookingRepository, start).Execute();
            return Ok(result);
        }

    }

}