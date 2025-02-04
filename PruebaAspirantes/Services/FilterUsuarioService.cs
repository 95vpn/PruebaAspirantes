using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class FilterUsuarioService : IFilterUsuarioService
    {
        private IFilterUsuarioRepository _filterUsuarioRepository;
        private IRepository<RolUsuario> _rolUsuarioRepository;
        private IRepository<Rol> _rolRepository;

        public FilterUsuarioService(IFilterUsuarioRepository filterUsuarioRepository,
            IRepository<RolUsuario> rolUsuarioRepository,
            IRepository<Rol> rolRepository)
        {
            _filterUsuarioRepository = filterUsuarioRepository; 
            _rolUsuarioRepository = rolUsuarioRepository;
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<SessionDto>> GetSesionesUsuario(int idUsuario, string token)
        {
            var sesion = await _filterUsuarioRepository.GetSesionesToken(token);

            if (sesion == null || sesion.FechaCierre != null)
            {
                throw new UnauthorizedAccessException("Session invalido o expirado");
            }
            var rol = await _filterUsuarioRepository.GetRol("Administrador");

            if (rol == null)
            {
                throw new Exception("Rol no encontrado");
            }

            var rolUsuario = await _rolUsuarioRepository.Get();
            var rolAsignado = rolUsuario.FirstOrDefault(ru => ru.IdUsuario == sesion.IdUsuario);

            if (rolAsignado != null && rolAsignado.IdRol == rol.IdRol )
            {
                var sesiones = await _filterUsuarioRepository.GetSesionesUsuario(idUsuario);
                return sesiones.Select(s => new SessionDto
                {
                    IdSession = s.IdSession,
                    FechaIngreso = s.FechaIngreso,
                    FechaCierre = s.FechaCierre,
                    IdUsuario = s.IdUsuario,
                    Token = s.Token
                });
            }
            throw new UnauthorizedAccessException("No tienes permisos para ver esta información.");
        }
    }
    
}

