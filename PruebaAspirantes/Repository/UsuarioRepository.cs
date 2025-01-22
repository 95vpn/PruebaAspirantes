using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private StoreContext _context;

        public UsuarioRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> Get() =>
            await _context.Usuarios.ToListAsync();


        public async Task<Usuario> GetById(int id) =>
            await _context.Usuarios.FindAsync(id);
       

        public async Task Add(Usuario usuario) =>
            await _context.Usuarios.AddAsync(usuario);
       

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
        }


        public void Delete(Usuario usuario) =>
            _context.Usuarios.Remove(usuario);
        



        public async Task Save()
            => await _context.SaveChangesAsync();

        
    }
}
