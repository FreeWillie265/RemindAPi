using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remind.Core.Models;
using Remind.Core.Services;

namespace RemindAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;
        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }
        
        // GET: api/Subjects
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Subjects/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Subjects
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
