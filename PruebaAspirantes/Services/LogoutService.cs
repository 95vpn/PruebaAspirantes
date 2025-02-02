using PruebaAspirantes.DTOs;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class LogoutService : ILogoutService
    {
        private ILoginRepository _logoutRepository;

        public LogoutService (ILoginRepository logoutRepository)
        {
            _logoutRepository = logoutRepository;
        }

        public async Task<string> Logout(LogoutDto logoutDto)
        {
            var session = await _logoutRepository.SessionActiva(logoutDto.IdUsuario);
            if (session == null)
            {
                return "No hay una sesión activa para este usuario.";
            }

            
            if (session.Token != logoutDto.Token)
            {
                return "Token inválido para esta sesión.";
            }

            
            session.FechaCierre = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); 
            _logoutRepository.Update(session);

            await _logoutRepository.Save();

            var usuario = await _logoutRepository.GetUsuarioById(logoutDto.IdUsuario);
            if (usuario != null)
            {
                usuario.SessionActive = "false";
                _logoutRepository.Update(usuario);
                await _logoutRepository.Save();
            }

            return "Sesión cerrada exitosamente.";
        }
    }
}
