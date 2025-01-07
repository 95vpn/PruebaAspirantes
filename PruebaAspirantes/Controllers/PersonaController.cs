using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using static Azure.Core.HttpHeader;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<PersonaInsertDto> _personaInsertValidator;
        public PersonaController(StoreContext context,
            IValidator<PersonaInsertDto> personaInsertValidator)
        {
            _context = context;
            _personaInsertValidator = personaInsertValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonaDto>> Get() =>
            await _context.Personas.Select(x => new PersonaDto
            {
                IdPersona = x.IdPersona,
                Names = x.Names,
                LastNames = x.LastNames,
                Identificacion = x.Identificacion,
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDto>> GetById(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Names = persona.Names,
                LastNames = persona.LastNames,
                Identificacion = persona.Identificacion,
            };

            return Ok(personaDto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaDto>> Add(PersonaInsertDto personaInsertDto)
        {
            var validationResult = await _personaInsertValidator.ValidateAsync(personaInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var persona = new Persona()
            {
                Names = personaInsertDto.Names,
                LastNames = personaInsertDto.LastNames,
                Identificacion = personaInsertDto.Identificacion,
            };

            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Names = persona.Names,
                LastNames = persona.LastNames,
                Identificacion = persona.Identificacion,
            };

            return CreatedAtAction(nameof(GetById), new { id = persona.IdPersona }, personaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaDto>> Update(int id, PersonaUpdateDto personaUpdateDto)
        {
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            persona.Names = personaUpdateDto.Names;
            persona.LastNames = personaUpdateDto.LastNames;
            persona.Identificacion = personaUpdateDto.Identificacion;
            await _context.SaveChangesAsync();

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Names = persona.Names,
                LastNames = persona.LastNames,
                Identificacion = persona.Identificacion,
            };
            return Ok(personaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
