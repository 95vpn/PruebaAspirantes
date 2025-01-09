using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Services;
using static Azure.Core.HttpHeader;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        
        private IValidator<PersonaInsertDto> _personaInsertValidator;
        private IValidator<PersonaUpdateDto> _personaUpdateValidator;
        private ICommonService<PersonaDto, PersonaInsertDto, PersonaUpdateDto> _personaService;
        public PersonaController(
            IValidator<PersonaInsertDto> personaInsertValidator,
            IValidator<PersonaUpdateDto> personaUpdateValidator,
            [FromKeyedServices("personaService")]ICommonService<PersonaDto, PersonaInsertDto, PersonaUpdateDto> personaService)
        {
            
            _personaInsertValidator = personaInsertValidator;
            _personaUpdateValidator = personaUpdateValidator;
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonaDto>> Get() =>
            await _personaService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDto>> GetById(int id)
        {
            var personaDto = await _personaService.GetById(id);

            return personaDto == null ? NotFound() : Ok(personaDto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaDto>> Add(PersonaInsertDto personaInsertDto)
        {
            var validationResult = await _personaInsertValidator.ValidateAsync(personaInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var personaDto = await _personaService.Add(personaInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = personaDto.IdPersona }, personaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaDto>> Update(int id, PersonaUpdateDto personaUpdateDto)
        {
            var validationResult = await _personaUpdateValidator.ValidateAsync(personaUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
 
            var personaDto = await _personaService.Update(id, personaUpdateDto);
            return personaDto == null? NotFound() : Ok(personaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonaDto>> Delete(int id)
        {
            var personaDto = await _personaService.Delete(id);
            return personaDto == null ? NotFound() : Ok(personaDto);
        }
    }
}
