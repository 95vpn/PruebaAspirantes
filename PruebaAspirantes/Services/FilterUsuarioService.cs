using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class FilterUsuarioService : IFilterUsuarioService
    {
        private IFilterUsuarioRepository _filterUsuarioRepository;
        private IRepository<RolUsuario> _rolUsuarioRepository;

        public FilterUsuarioService(IFilterUsuarioRepository filterUsuarioRepository,
            IRepository<RolUsuario> rolUsuarioRepository)
        {
            _filterUsuarioRepository = filterUsuarioRepository; 
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        public async Task<IEnumerable<SessionDto>> GetSesionesUsuario(int idUsuario, string token)
        {
            var sesion = await _filterUsuarioRepository.GetSesionesUsuario(token);

            if (sesion == null || sesion.FechaCierre != null)
            {
                throw new UnauthorizedAccessException("Session invalido o expirado");
            }

            var rolUsuario = await _rolUsuarioRepository.Get();
            var rol = rolUsuario.FirstOrDefault(ru => ru.IdUsuario == sesion.IdUsuario);

            if (rol != null && rol.IdRol == )
            {

            }
        }
    }
}
