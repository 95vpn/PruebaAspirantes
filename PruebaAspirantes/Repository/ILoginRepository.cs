using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface ILoginRepository
    {
        Task<Usuario?> GetUsuarioByEmailAndPassword(string email, string password);

        Task<Usuario?> GetUsuarioByEmail(string email);

        Task<Usuario?> GetUsuarioById(int id);

        Task<Session?> SessionActiva(int id);

        Task Add(Session session);

        void Update(Usuario usuario);

        void Update(Session session);

        Task Save();
    }
}
