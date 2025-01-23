using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class PersonaService : ICommonService<PersonaDto, PersonaInsertDto, PersonaUpdateDto>
    {
        
        //private IRepository<Persona> _personaRepository;
        private IPersonaRepository _personaRepository;

        public PersonaService(
            IPersonaRepository personaRepository)
        {
            
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<PersonaDto>> Get()
        {
            var personas = await _personaRepository.Get();
            return personas.Select(p => new PersonaDto()
            {
               
                IdPersona = p.IdPersona,
                Names = p.Names,
                LastNames = p.LastNames,
                Identificacion = p.Identificacion,
            
            });
        }

        public async Task<PersonaDto> GetById(int id)
        {
            var persona = await _personaRepository.GetById(id);

            if (persona != null)
            {
                var personaDto = new PersonaDto
                {
                    IdPersona = persona.IdPersona,
                    Names = persona.Names,
                    LastNames = persona.LastNames,
                    Identificacion = persona.Identificacion,
                };

                return personaDto;
            }

            return null;
        }
        /*
        public async Task<PersonaDto> GetById(params object[] keyValues)
        {
            var persona = await _personaRepository.GetById(keyValues);

            if (persona != null)
            {
                var personaDto = new PersonaDto
                {
                    IdPersona = persona.IdPersona,
                    Names = persona.Names,
                    LastNames = persona.LastNames,
                    Identificacion = persona.Identificacion,
                };

                return personaDto;
            }

            return null;
        }
        */

        public async Task<PersonaDto> Add(PersonaInsertDto personaInsertDto)
        {

            var existeIdentificacion = await _personaRepository.ExisteIdentificacion(personaInsertDto.Identificacion);

            if  (existeIdentificacion)
            {
                throw new Exception("La identificacion ya existe en el sistema");
            }

            var persona = new Persona()
            {
                Names = personaInsertDto.Names,
                LastNames = personaInsertDto.LastNames,
                Identificacion = personaInsertDto.Identificacion,
            };

            await _personaRepository.Add(persona);
            await _personaRepository.Save();

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Names = persona.Names,
                LastNames = persona.LastNames,
                Identificacion = persona.Identificacion,
            };
            return personaDto;
        }

        public async Task<PersonaDto> Update(int id, PersonaUpdateDto personaUpdateDto)
        {
            var persona = await _personaRepository.GetById(id);

            if (persona != null)
            {
                persona.Names = personaUpdateDto.Names;
                persona.LastNames = personaUpdateDto.LastNames;
                persona.Identificacion = personaUpdateDto.Identificacion;

                _personaRepository.Update(persona);
                await _personaRepository.Save();

                var personaDto = new PersonaDto
                {
                    IdPersona = persona.IdPersona,
                    Names = persona.Names,
                    LastNames = persona.LastNames,
                    Identificacion = persona.Identificacion,
                };
                return personaDto;
            }
            return null;
        }

        public async Task<PersonaDto> Delete(int id)
        {
            var persona = await _personaRepository.GetById(id);

            if (persona != null)
            {
                var personaDto = new PersonaDto
                {
                    IdPersona = persona.IdPersona,
                    Names = persona.Names,
                    LastNames = persona.LastNames,
                    Identificacion = persona.Identificacion,
                };
                _personaRepository.Delete(persona);
                await _personaRepository.Save();

                
                return personaDto;
            }
            return null;
        }

        
    }
}
