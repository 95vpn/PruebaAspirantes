using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<bool> ExisteIdentificacion(string identificacion);
    }
}
