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
            var rolUsuarios = await _rolUsuarioRepository.Get();
            var existeRelacion = rolUsuarios.Any(ru => 
                ru.IdRol == rolUsuarioInsertDto.IdRol && ru.IdUsuario == rolUsuarioInsertDto.IdUsuario);

            if (existeRelacion) 
            {
                throw new InvalidOperationException("La relacion ya existe");
            }


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

        public async Task<RolUsuarioDto> Update(int id, RolUsuarioUpdateDto rolUsuarioUpdateDto)
        {
            var rolUsuarios = await _rolUsuarioRepository.Get();
            var existeRelacion = rolUsuarios.Any(ru =>
                ru.IdRol == rolUsuarioUpdateDto.IdRol && ru.IdUsuario == rolUsuarioUpdateDto.IdUsuario);

            if (existeRelacion)
            {
                throw new InvalidOperationException("La relacion ya existe");
            }

            var rolUsuario = await _rolUsuarioRepository.GetById(id);
            if (rolUsuario != null)
            {
                rolUsuario.IdRol = rolUsuarioUpdateDto.IdRol;
                rolUsuario.IdUsuario = rolUsuarioUpdateDto.IdUsuario;

                _rolUsuarioRepository.Update(rolUsuario);
                await _rolUsuarioRepository.Save();

                var rolUsuarioDto = new RolUsuarioDto()
                {
                    Id = rolUsuario.Id,
                    IdRol = rolUsuario.IdRol,
                    IdUsuario = rolUsuario.IdUsuario,
                };

                return rolUsuarioDto;
            }
            return null;
        }

        public async Task<RolUsuarioDto> Delete(int id)
        {
            var rolUsuario = await _rolUsuarioRepository.GetById(id);

            if (rolUsuario != null)
            {
                var rolUsuarioDto = new RolUsuarioDto()
                {
                    Id = rolUsuario.Id,
                    IdRol = rolUsuario.IdRol,
                    IdUsuario = rolUsuario.IdUsuario
                };

                _rolUsuarioRepository.Delete(rolUsuario);
                await _rolUsuarioRepository.Save();

                return rolUsuarioDto;
            }
            return null;
        }

    }
}
