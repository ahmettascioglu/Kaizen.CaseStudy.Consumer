using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Dtos;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Kaizen.CaseStudy.Consumer.Services.ConsumerBService;
using Kaizen.CaseStudy.Consumer.Services.SmsService;
using Kaizen.CaseStudy.Consumer.WebAPI.Validators;

namespace Kaizen.CaseStudy.Consumer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerBController : ControllerBase
    {
        private readonly IConsumerBService _consumerBService;
        private readonly ISmsService _smsService;
        public ConsumerBController(IConsumerBService consumerBService, ISmsService smsService)
        {
            this._consumerBService = consumerBService;
            this._smsService = smsService;
        }

        /// <summary>
        /// Gets Consumer Entity By Given Id
        /// </summary>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerB))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerB>> Get(int id)
        {
            var consumer = await _consumerBService.GetConsumerB(id);

            return consumer;
        }
        /// <summary>
        /// Insert New Consumer
        /// </summary>
        /// <param name="model">New Consumer Entity</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConsumerB))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsumerB>> Post([FromBody] ConsumerB model)
        {

            var validator = new ConsumerBValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var code = Guid.NewGuid().ToString().Split("-")[0];

            _smsService.SendSms(code, model.PhoneNumber);
            model.IsValidated = false;
            var consumer = _consumerBService.CreateConsumerB(model);
            return CreatedAtAction(nameof(Get), new { id = consumer.Id }, consumer);


        }

        [HttpPost]
        [Route("{id}/validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerB))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerB>> Validate([FromBody] SmsValidationDto model)
        {
            var smsServiceResult = _smsService.IsValid(model.Code, model.PhoneNumber);
            if (smsServiceResult)
            {
                var consumer = await _consumerBService.GetConsumerB(model.ConsumerId);
                if (consumer != null)
                {
                    consumer.IsValidated = true;
                    var res = await _consumerBService.UpdateConsumer(consumer);
                    return Ok(res);
                }

                return NotFound();
            }

            return BadRequest();
        }

        /// <summary>
        /// Update consumer via given id and updated entity
        /// </summary>
        /// <param name="model">Updated Consumer Entity</param>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerB))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerB>> Put([FromBody] ConsumerB model, int id)
        {
            var oldConsumer = _consumerBService.GetConsumerB(id).Result;
            if (oldConsumer != null)
            {

                var validator = new ConsumerBValidator();
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var consumer = _consumerBService.UpdateConsumer(model);
                return Ok(consumer.Result);
            }

            return NotFound();
        }


        /// <summary>
        /// Delete consumer from database by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerB))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var consumer = _consumerBService.GetConsumerB(id).Result;
            if (consumer == null)
                return NotFound();

            var res = _consumerBService.DeleteConsumer(consumer);
            if (res.IsCompletedSuccessfully)
                return Ok();

            return BadRequest();
        }
    }
}
