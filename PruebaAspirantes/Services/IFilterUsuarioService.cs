using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Services
{
    public interface IFilterUsuarioService
    {
        Task<IEnumerable<SessionDto>> GetSesionesUsuario(int idUsuario, string token);
    }
}
