using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperPractData.Models;
using DapperPractData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperPract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/<PersonController>
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _personRepository.GetPeople();
            return Ok(people);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            if (person is null)
                return NotFound();
            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            var result = await _personRepository.AddPerson(person);
            if (!result)
                return BadRequest("Could not save data");
            return Ok();
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            var person = _personRepository.GetPersonById(id);
            if (person is null)
                return NotFound();
            newPerson.Id = id;
            var result = await _personRepository.UpdatePerson(newPerson);
            if (!result)
                return BadRequest("Could not save data");
            return Ok();
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
          
            var person = _personRepository.GetPersonById(id);
            if (person is null)
                return NotFound();
            var result = await _personRepository.DeletePerson(id);
            if (!result)
                return BadRequest("Could not save data");
            return Ok();
        }
    }
}
