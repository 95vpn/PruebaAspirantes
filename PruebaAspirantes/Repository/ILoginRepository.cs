using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface ILoginRepository
    {
        Usuario ? GetUsuarioByEmailAndPassword(string email, string password);

        Usuario ? GetUsuarioById(int id);

        Session ? SessionActiva(int id);

        Task Add(Session session);

        void Update(Usuario usuario);
       
        Task Save();
    }
}
