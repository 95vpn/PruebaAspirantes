using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface ILoginRepository
    {
        Usuario ? GetUsuarioByEmailAndPassword(string email, string password);

        Usuario ? GetUsuarioByEmail(string email);

        Usuario ? GetUsuarioById(int id);

        Session ? SessionActiva(int id);

        Task Add(Session session);

        void Update(Usuario usuario);

        void Update(Session session);

        Task Save();
    }
}
