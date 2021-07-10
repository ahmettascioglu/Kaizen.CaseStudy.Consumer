using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.CaseStudy.Consumer.Core.Models;
using Kaizen.CaseStudy.Consumer.Services.ConsumerAService;
using Kaizen.CaseStudy.Consumer.Services.MernisService;
using Kaizen.CaseStudy.Consumer.WebAPI.Validators;

namespace Kaizen.CaseStudy.Consumer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerAController : ControllerBase
    {
        private readonly IConsumerAService _consumerAService;
        private readonly IMernisService _mernisService;
        public ConsumerAController(IConsumerAService consumerAService, IMernisService mernisService)
        {
            this._consumerAService = consumerAService;
            this._mernisService = mernisService;
        }

        /// <summary>
        /// Gets Consumer Entity By Given Id
        /// </summary>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerA))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerA>> Get(int id)
        {
            var consumer = await _consumerAService.GetConsumerA(id);

            return consumer;
        }
        /// <summary>
        /// Insert New Consumer
        /// </summary>
        /// <param name="model">New Consumer Entity</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConsumerA))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsumerA>> Post([FromBody] ConsumerA model)
        {

            var mernisValidationResult = _mernisService.Validate(model);

            if (mernisValidationResult.Result)
            {
                var validator = new ConsumerAValidator();
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var consumer = _consumerAService.CreateConsumerA(model);
                return CreatedAtAction(nameof(Get), new { id = consumer.Id }, consumer);
            }
            else
            {
                return BadRequest("Tc Kimlik Numarası Doğrulanamadı");
            }
        }

        /// <summary>
        /// Update consumer via given id and updated entity
        /// </summary>
        /// <param name="model">Updated Consumer Entity</param>
        /// <param name="id">Consumer Primary Key</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerA))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsumerA>> Put([FromBody] ConsumerA model, int id)
        {
            var oldConsumer = _consumerAService.GetConsumerA(id).Result;
            if (oldConsumer != null)
            {
                var validator = new ConsumerAValidator();
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var consumer = _consumerAService.UpdateConsumer(model);
                return Ok(consumer.Result);
            }

            return NotFound();
        }


        /// <summary>
        /// Delete consumerA from database by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConsumerA))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var consumer = _consumerAService.GetConsumerA(id).Result;
            if (consumer == null)
                return NotFound();

            var res = _consumerAService.DeleteConsumer(consumer);
            if (res.IsCompletedSuccessfully)
                return Ok();

            return BadRequest();
        }
    }
}
