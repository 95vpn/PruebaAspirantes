using Microsoft.EntityFrameworkCore;
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

        

        public Usuario ? GetUsuarioByEmailAndPassword(string email, string password)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public Usuario ? GetUsuarioById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public void RegisterSession(Session session)
        {
            _context.Sessions.Add(session);
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
        }

        public async Task Save() =>
        
            await _context.SaveChangesAsync();

        public async Task Add(Session session) =>
            await _context.Sessions.AddAsync(session);

        public Session? SessionActiva(int id)
        {
            return _context.Sessions.FirstOrDefault(s => s.IdUsuario == id && s.FechaCierre == null );
        }
    }
}
