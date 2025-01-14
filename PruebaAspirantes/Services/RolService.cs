using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class RolService : ICommonService<RolDto, RolIsertDto, RolUpdateDto>
    {
        
        private IRepository<Rol> _rolRepository;

        public RolService(
            IRepository<Rol> rolRepository)
        {
            
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<RolDto>> Get()
        {
            var roles = await _rolRepository.Get();

            return roles.Select(r => new RolDto()
            {

                IdRol = r.IdRol,
                RolName = r.RolName

            });
            
        }
            

        public async Task<RolDto> GetById(int id)
        {
            var rol = await _rolRepository.GetById(id);

            if (rol != null)
            {
                var rolDto = new RolDto
                {
                    IdRol = rol.IdRol,
                    RolName = rol.RolName
                };
                return rolDto;
            }
            return null;
        }

        public async Task<RolDto> Add(RolIsertDto rolInsertDto)
        {
            var rol = new Rol()
            {
                RolName = rolInsertDto.RolName
            };

            await _rolRepository.Add(rol);
            await _rolRepository.Save();

            var rolDto = new RolDto
            {
                IdRol = rol.IdRol,
                RolName = rol.RolName
            };
            return rolDto;
        }


        public async Task<RolDto> Update(int id, RolUpdateDto rolUpdateDto)
        {
            var rol = await _rolRepository.GetById(id);
            if (rol != null)
            {
                rol.RolName = rolUpdateDto.RolName;

                _rolRepository.Update(rol);
                await _rolRepository.Save();

                var rolDto = new RolDto
                {
                    IdRol = rol.IdRol,
                    RolName = rol.RolName
                };

                return rolDto;

            }
            return null;
        }


        public async Task<RolDto> Delete(int id)
        {
            var rol = await _rolRepository.GetById(id);
            if (rol != null)
            {
                var rolDto = new RolDto
                {
                    IdRol = rol.IdRol,
                    RolName = rol.RolName
                };

                _rolRepository.Delete(rol);
                await _rolRepository.Save();

                return rolDto;

            }
            return null;
        }




    }
}
