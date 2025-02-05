using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface IFilterUsuarioRepository
    {
        Task<Usuario?> GetIdUsuario(int idUsuario);
        Task<IEnumerable<Session>> GetSesionesIdUsuario(int idUsuario);
        Task<Usuario?> GetUsuarioRoles(int idUsuario);
    }
}
