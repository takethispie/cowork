using System;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cowork.Controllers.Cowork {

    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase {

        public ISubscriptionRepository Repository;
        public ITimeSlotRepository TimeSlotRepository;


        public SubscriptionController(ISubscriptionRepository repository, ITimeSlotRepository timeSlotRepository) {
            Repository = repository;
            TimeSlotRepository = timeSlotRepository;
        }


        [HttpGet]
        public IActionResult All() {
            var result = Repository.GetAll().Select(sub => {
                sub.Place.OpenedTimes = TimeSlotRepository.GetAllOfPlace(sub.Place.Id);
                return sub;
            });
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Subscription sub) {
            var res = Repository.Create(sub);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Subscription sub) {
            var res = Repository.Update(sub);
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = Repository.Delete(id);
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = Repository.GetById(id);
            if (result == null) return NotFound();
            result.Place.OpenedTimes = TimeSlotRepository.GetAllOfPlace(result.Place.Id);
            return Ok(result);
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult OfUser(long userId) {
            var res = Repository.GetOfUser(userId);
            if (res == null) return NotFound();
            res.Place.OpenedTimes = TimeSlotRepository.GetAllOfPlace(res.Place.Id);
            if (res.FixedContract &&
                res.LatestRenewal.AddMonths(res.Type.FixedContractDurationMonth) < DateTime.Today) {
                Repository.Delete(res.Id);
                return Ok(null);
            }
            return Ok(res);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = Repository.GetAllWithPaging(page, amount);
            return Ok(result);
        }

    }

}