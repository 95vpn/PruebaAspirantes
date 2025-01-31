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

        

        public async Task<Usuario?> GetUsuarioByEmailAndPassword(string email, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<Usuario?> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<Session?> SessionActiva(int id)
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.IdUsuario == id && s.FechaCierre == null);
        }

        /*
        public void RegisterSession(Session session)
        {
            _context.Sessions.Add(session);
        }
        */

        public async Task Add(Session session) =>
            await _context.Sessions.AddAsync(session);

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
        }

        public void Update(Session session)
        {
       
            _context.Sessions.Attach(session);
            _context.Sessions.Entry(session).State = EntityState.Modified;
        }


        public async Task Save() =>

            await _context.SaveChangesAsync();


    }
}
