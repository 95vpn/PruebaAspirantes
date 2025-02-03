using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface IFilterUsuarioRepository
    {
        Task<Session?> GetSesionesUsuario(string token);
    }
}
