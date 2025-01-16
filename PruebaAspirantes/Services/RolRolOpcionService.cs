using PruebaAspirantes.DTOs;
using PruebaAspirantes.Migrations;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;
using RolRolOpcion = PruebaAspirantes.Models.RolRolOpcion;

namespace PruebaAspirantes.Services
{
    public class RolRolOpcionService : ICommonService<RolRolOpcionDto, RolRolOpcionInsertDto, RolRolOpcionUpdateDto>
    {

        private IRolRolOpcionRepository _rolRolOpcionRepository;
        private IRepository<RolOpcion> _rolOpcionRepository;

        public RolRolOpcionService(IRolRolOpcionRepository rolRolOpcionRepository,
            IRepository<RolOpcion> rolOpcionRepository)
        {
            _rolRolOpcionRepository = rolRolOpcionRepository;
            _rolOpcionRepository = rolOpcionRepository;
        }

        public async Task<IEnumerable<RolRolOpcionDto>> Get()
        {
            var rolRolOpciones = await _rolRolOpcionRepository.Get();

            return rolRolOpciones.Select(rro => new RolRolOpcionDto()
            {
                Id = rro.Id,
                IdRol = rro.IdRol,
                IdOption = rro.IdOption
            });
        }
        
        public async Task<RolRolOpcionDto> GetById(int id)
        {
            var rolRolOption = await _rolRolOpcionRepository.GetById(id);

            if (rolRolOption != null)
            {
                var rolRolOptionDto = new RolRolOpcionDto()
                {
                    Id = rolRolOption.Id,
                    IdRol = rolRolOption.IdRol,
                    IdOption = rolRolOption.IdOption
                };
                return rolRolOptionDto;
            }
            return null;
        }
        
        /*
        public async Task<RolRolOpcionDto> GetById(params object[] keyValues)
        {
            var rolRolOption = await _rolRolOpcionRepository.GetById(keyValues);

            if (rolRolOption != null)
            {
                var rolRolOptionDto = new RolRolOpcionDto()
                {
                    Id = rolRolOption.Id,
                    IdRol = rolRolOption.IdRol,
                    IdOption = rolRolOption.IdOption
                };
                return rolRolOptionDto;
            }
            return null;
        }
        */
        public async Task<RolRolOpcionDto> Add(RolRolOpcionInsertDto rolRolInsertDto)
        {
            var existeRol = await _rolRolOpcionRepository.ExisteRol(rolRolInsertDto.IdRol);
            if (!existeRol)
            {
                throw new KeyNotFoundException($"el rol con Id {rolRolInsertDto.IdRol} no existe");
            }

            var existeOpcion = await _rolRolOpcionRepository.ExisteOpcion(rolRolInsertDto.IdOption);
            if (!existeOpcion)
            {
                throw new KeyNotFoundException($"la opcion con ID {rolRolInsertDto.IdOption} no existe ");
            }

            var rolRolOpcion = new RolRolOpcion()
            {
                
                IdRol = rolRolInsertDto.IdRol,
                IdOption = rolRolInsertDto.IdOption
            };

            await _rolRolOpcionRepository.Add(rolRolOpcion);
            await _rolRolOpcionRepository.Save();

            var rolRolOpcionDto = new RolRolOpcionDto()
            {
                Id = rolRolOpcion.Id,
                IdRol = rolRolOpcion.IdRol,
                IdOption = rolRolOpcion.IdOption
            };

            return rolRolOpcionDto;
        }

        public async Task<RolRolOpcionDto> Update(int id, RolRolOpcionUpdateDto rolRolUpdateDto)
        {
            var rolRolOpcion = await _rolRolOpcionRepository.GetById(id);

            if (rolRolOpcion != null)
            {
                
                rolRolOpcion.IdRol = rolRolUpdateDto.IdRol;
                rolRolOpcion.IdOption = rolRolUpdateDto.IdOption;

                _rolRolOpcionRepository.Update(rolRolOpcion);
                await _rolOpcionRepository.Save();

                var rolRolDto = new RolRolOpcionDto()
                {
                    Id = rolRolOpcion.Id,
                    IdRol = rolRolOpcion.IdRol,
                    IdOption = rolRolOpcion.IdOption
                };
                return rolRolDto;
            }
            return null;
        }

        public async Task<RolRolOpcionDto> Delete(int id)
        {
            var rolRolOption = await _rolRolOpcionRepository.GetById(id);

            if (rolRolOption != null)
            {
                var rolRolOptionDto = new RolRolOpcionDto()
                {
                    Id = rolRolOption.Id,
                    IdRol = rolRolOption.IdRol,
                    IdOption = rolRolOption.IdOption,

                };

                _rolRolOpcionRepository.Delete(rolRolOption);
                await _rolOpcionRepository.Save();

                return rolRolOptionDto;
            }
            return null;
        }

        

        

        
    }
}
