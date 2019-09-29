using System;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.usecases.Subscription;
using cowork.usecases.Subscription.Models;
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
            var result = new GetAllSubscriptions(Repository, TimeSlotRepository).Execute();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateSubscriptionInput sub) {
            var res = new CreateSubscription(Repository, sub).Execute();
            if (res == -1) return Conflict();
            return Ok(res);
        }


        [HttpPut]
        public IActionResult Update([FromBody] Subscription sub) {
            var res = new UpdateSubscription(Repository, sub).Execute();
            if (res == -1) return BadRequest("Impossible de creer l'abonnement");
            return Ok(res);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var result = new DeleteSubscription(Repository, id).Execute();
            if (!result) return NotFound();
            return Ok();
        }


        [HttpGet("ById/{id}")]
        public IActionResult ById(long id) {
            var result = new GetSubscriptionById(Repository, TimeSlotRepository, id).Execute();
            return Ok(result);
        }


        [HttpGet("OfUser/{userId}")]
        public IActionResult OfUser(long userId) {
            var res = new GetSubscriptionOfUser(Repository, TimeSlotRepository, userId).Execute();
            return Ok(res);
        }


        [HttpGet("WithPaging/{page}/{amount}")]
        public IActionResult AllWithPaging(int page, int amount) {
            var result = new GetSubscriptionsWithPaging(Repository, page, amount).Execute();
            return Ok(result);
        }

    }

}