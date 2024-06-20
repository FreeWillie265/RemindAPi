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

        // GET api/<ObservationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ObservationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ObservationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ObservationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
