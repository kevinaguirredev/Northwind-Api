using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindApi.IRepository;
using NorthwindApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindApi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<Person> logger;
        private readonly IMapper mapper;

        public PersonController(IUnitOfWork unitOfWork, ILogger<Person> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersons()
        {

            try
            {
                var persons = await unitOfWork.Persons.GetAll();
                var results = mapper.Map<IList<PersonDTO>>(persons);
                return Ok(results);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Something went wrong in the {nameof(GetPersons)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }

        }

        [HttpGet("{id:int}", Name = "GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPerson(int id)
        {

            try
            {
                var person = await unitOfWork.Persons.Get(p => p.PersonId == id);
                var result = mapper.Map<PersonDTO>(person);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Something went wrong in the {nameof(GetPerson)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDTO personDTO)
        {

            if (!ModelState.IsValid)
            {
                this.logger.LogError($"Invalid POST attempt in {nameof(CreatePerson)}");
                return BadRequest(ModelState);
            }

            try
            {
                var person = mapper.Map<Person>(personDTO);
                person.CreatedDate = DateTime.Now;
                await unitOfWork.Persons.Insert(person);
                await unitOfWork.Save();

                var personDto = mapper.Map<PersonDTO>(person);

                return CreatedAtRoute("GetPerson", new { id = person.PersonId }, personDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Something went wrong in the {nameof(CreatePerson)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDTO personDTO)
        {

            if (!ModelState.IsValid || id < 0)
            {
                logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePerson)}");
                return BadRequest(ModelState);
            }

            try
            {
                var person = await unitOfWork.Persons.Get(q => q.PersonId == id);

                if (person == null)
                {
                    logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePerson)}");
                    return BadRequest("Person was not found. Please provide a valid id value.");
                }

                mapper.Map(personDTO, person);
                person.ModifiedDate = DateTime.Now;
                unitOfWork.Persons.Update(person);
                await unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Something went wrong in the {nameof(UpdatePerson)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerson(int id)
        {

            if (id < 1)
            {
                this.logger.LogError($"Invalid delete attempt in {nameof(DeletePerson)}");
                return BadRequest();
            }

            try
            {
                var person = await unitOfWork.Persons.Get(p => p.PersonId == id);

                if (person == null)
                {
                    this.logger.LogError($"Invalid delete attempt in {nameof(DeletePerson)}");
                    return BadRequest("User not found. Please provide a valid user.");
                }

                await unitOfWork.Persons.Delete(id);
                await unitOfWork.Save();

                return NoContent();

            }
            catch (Exception ex)
            {

                this.logger.LogError(ex, $"Invalid delete attempt in {nameof(DeletePerson)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");

            }

        }

    }
}
