using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Dtos;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Kaizen.CaseStudy.Consumer.Services.ConsumerCService;
using Kaizen.CaseStudy.Consumer.Services.MailService;
using Kaizen.CaseStudy.Consumer.WebAPI.Validators;

namespace Kaizen.CaseStudy.Consumer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerCController : ControllerBase
    {
        private readonly IConsumerCService _consumerCService;
        private readonly IMailService _mailService;
        public ConsumerCController(IConsumerCService consumerCService, IMailService mailService)
        {
            this._consumerCService = consumerCService;
            this._mailService = mailService;
        }

        /// <summary>
        /// Gets Consumer Entity By Given Id
        /// </summary>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerC))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerC>> Get(int id)
        {
            var consumer = await _consumerCService.GetConsumerC(id);

            return consumer;
        }
        /// <summary>
        /// Insert New Consumer
        /// </summary>
        /// <param name="model">New Consumer Entity</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConsumerC))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsumerC>> Post([FromBody] ConsumerC model)
        {
            var validator = new ConsumerCValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var code = Guid.NewGuid().ToString().Split("-")[0];
            _mailService.SendMail(code, model.Email);
            model.IsValidated = false;

            var consumer = _consumerCService.CreateConsumerC(model);
            return CreatedAtAction(nameof(Get), new { id = consumer.Id }, consumer);


        }

        /// <summary>
        /// Update consumer via given id and updated entity
        /// </summary>
        /// <param name="model">Updated Consumer Entity</param>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerC))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerC>> Put([FromBody] ConsumerC model, int id)
        {
            var oldConsumer = _consumerCService.GetConsumerC(id).Result;
            if (oldConsumer != null)
            {
                var validator = new ConsumerCValidator();
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var consumer = _consumerCService.UpdateConsumer(model);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerC))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var consumer = _consumerCService.GetConsumerC(id).Result;
            if (consumer == null)
                return NotFound();

            var res = _consumerCService.DeleteConsumer(consumer);
            if (res.IsCompletedSuccessfully)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("{id}/validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerC))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerC>> Validate([FromBody] MailValidationDto model)
        {
            var smsServiceResult = _mailService.IsValid(model.Code, model.Email);
            if (smsServiceResult)
            {
                var consumer = await _consumerCService.GetConsumerC(model.ConsumerId);
                if (consumer != null)
                {
                    consumer.IsValidated = true;
                    var res = await _consumerCService.UpdateConsumer(consumer);
                    return Ok(res);
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
