using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idUsuarioSolicitante = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var roles = jwtToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Verificar permisos: Admin o el mismo usuario
            bool esAdmin = roles.Contains("Administrador");
            bool esMismoUsuario = idUsuarioSolicitante == idUsuario;

            if (!esAdmin && !esMismoUsuario)
            {
                throw new UnauthorizedAccessException("No tienes permisos para ver estas sesiones.");
            }

            var sesiones = await _filterUsuarioRepository.GetSesionesIdUsuario(idUsuario);

            if (sesiones == null || !sesiones.Any())
            {
                throw new Exception("No se encontraron sesiones para este usuario.");
            }

            // Obtener las sesiones del usuario solicitado
            var sesionesDto = sesiones.Select(s => new SessionDto
            {
                IdSession = s.IdSession,
                FechaIngreso = s.FechaIngreso,
                FechaCierre = s.FechaCierre,
                IdUsuario = s.IdUsuario,
                Token = s.Token
            }).ToList();  // Convertir a lista si se requiere

            return sesionesDto;
        }
    }
    
}

