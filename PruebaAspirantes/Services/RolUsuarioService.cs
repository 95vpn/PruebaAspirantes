using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class RolUsuarioService : ICommonService<RolUsuarioDto, RolUsuarioInsertDto, RolUsuarioUpdateDto>
    {
        private IRepository<RolUsuario> _rolUsuarioRepository;

        public RolUsuarioService(IRepository<RolUsuario> rolUsuarioRepository)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        public async Task<IEnumerable<RolUsuarioDto>> Get()
        {
            var rolUsuarios = await _rolUsuarioRepository.Get();

            return rolUsuarios.Select(ru => new RolUsuarioDto()
            {
                Id = ru.Id,
                IdRol = ru.IdRol,
                IdUsuario = ru.IdUsuario,
            });
        }

        public async Task<RolUsuarioDto> GetById(int id)
        {
            var rolUsuario = await _rolUsuarioRepository.GetById(id);

            if (rolUsuario != null)
            {
                var rolUsuariDto = new RolUsuarioDto()
                {
                    Id = rolUsuario.Id,
                    IdRol = rolUsuario.IdRol,
                    IdUsuario = rolUsuario.IdUsuario,
                };
                return rolUsuariDto;
            }

            return null;
        }

        public async Task<RolUsuarioDto> Add(RolUsuarioInsertDto rolUsuarioInsertDto)
        {
            var rolUsuario = new RolUsuario()
            {
                IdRol = rolUsuarioInsertDto.IdRol,
                IdUsuario = rolUsuarioInsertDto.IdUsuario,
            };

            await _rolUsuarioRepository.Add(rolUsuario);
            await _rolUsuarioRepository.Save();

            var rolUsuarioDto = new RolUsuarioDto()
            {
                Id = rolUsuario.Id,
                IdRol = rolUsuario.IdRol,
                IdUsuario = rolUsuario.IdUsuario,
            };

            return rolUsuarioDto;
        }

        public Task<RolUsuarioDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        

        public Task<RolUsuarioDto> Update(int id, RolUsuarioUpdateDto personaUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
