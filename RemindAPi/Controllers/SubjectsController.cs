using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Remind.Core.Models;
using Remind.Core.Services;
using RemindAPi.Resources;

namespace RemindAPi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;
        private readonly IMapper _mapper;
        public SubjectsController(ISubjectService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> Get()
        {
            var subjects = await _service.GetAll();
            return Ok(subjects);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Subject>> Get(Guid id)
        {
            var subject = await _service.GetById(id);
            if (subject == null)
                return NotFound();
            return Ok(subject);
        }

        // POST: api/Subjects/GetNext
        [HttpPost("GetNext")]
        public async Task<ActionResult> GetNext([FromBody] SubjectRequestResource resource)
        {
            var subject = await _service.GetNext(resource.AgeGroup, resource.Sex);
            if (subject == null)
                return BadRequest("No available slots for this (AGE GROUP - SEX) combination");
            var updatesResource = new SaveSubjectResource()
            {
                AgeGroup = subject.AgeGroup,
                BlockId = subject.BlockId,
                BlockSize = subject.BlockSize,
                Treatment = subject.Treatment,
                Clerk = resource.Clerk,
                ClinicName = resource.ClinicName,
                District = resource.District,
                Etc = resource.Etc,
                Sex = subject.Sex,
                Traversed = true
            };

            var updates = _mapper.Map<SaveSubjectResource, Subject>(updatesResource);
            await _service.UpdateSubject(subject, updates);
            
            var updatedSubject = await _service.GetById(subject.Id);
            return Ok(updatedSubject);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> Put(Guid id, [FromBody] SaveSubjectResource resource)
        {
            var subject = await _service.GetById(id);
            if (subject == null)
                return NotFound();

            var updates = _mapper.Map<SaveSubjectResource, Subject>(resource);

            await _service.UpdateSubject(subject, updates);

            var updatedSubject = await _service.GetById(id);

            return Ok(updatedSubject);
        }

        /*// DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
