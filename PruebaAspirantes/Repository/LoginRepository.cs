using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class LoginRepository : ILoginRepository
    {

        private StoreContext _context;

        public LoginRepository(StoreContext context)
        {
            _context = context;
        }

        public Usuario? GetUsuarioByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario? GetUsuarioByEmailAndPassword(string email, string password)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
