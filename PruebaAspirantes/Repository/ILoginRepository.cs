using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public interface ILoginRepository
    {
        Usuario ? GetUsuarioByEmailAndPassword(string email, string password);

        
    }
}
