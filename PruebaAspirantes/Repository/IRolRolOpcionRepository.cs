using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface IRolRolOpcionRepository : IRepository<RolRolOpcion>
    {
        Task<bool> ExisteRol(int idRol);
        Task<bool> ExisteOpcion(int idOption);
    }
}
