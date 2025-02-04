using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface IFilterUsuarioRepository
    {
        Task<IEnumerable<Session?>> GetSessions();

        Task<IEnumerable<Rol?>> GetRol();
    }
}
