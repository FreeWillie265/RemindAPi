using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remind.Core.Models;
using Remind.Core.Services;

namespace RemindAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationsController(IObservationService service, IMapper mapper) : ControllerBase
    {

        // GET: api/Observations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Observation>>> Get()
        {
            var observations = await service.GetAll();
            return Ok(observations);
        }

        // GET api/Observations/5
        [HttpGet("{id}", Name = "GetObservation")]
        public async Task<ActionResult<Observation>> Get(Guid id)
        {
            var observation = await service.GetById(id);
            if (observation == null)
                return NotFound();
            return Ok(observation);
        }

        // POST api/Observations/GetNext
        [HttpPost("GetNext")]
        public async Task<ActionResult> GetNext()
        {
            var observation = await service.GetNext();
            if (observation == null)
                return Ok("0");

            var processedObservation = await service.ProcessObservation(observation);
            return Ok(processedObservation);
        }
    }
}
