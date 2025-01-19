using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class RolUsuarioRepository : IRepository<RolUsuario>
    {

        private StoreContext _context;

        public RolUsuarioRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolUsuario>> Get() =>
            await _context.RolUsuarios.ToListAsync();

        public async Task<RolUsuario> GetById(int id) =>
            await _context.RolUsuarios.FindAsync(id);

        public async Task Add(RolUsuario rolUsuario) =>
            await _context.RolUsuarios.AddAsync(rolUsuario);

        public void Update(RolUsuario rolUsuario)
        {
            _context.RolUsuarios.Attach(rolUsuario);
            _context.RolUsuarios.Entry(rolUsuario).State = EntityState.Modified;
        }

        public void Delete(RolUsuario rolUsuario) =>
            _context.RolUsuarios.Remove(rolUsuario);

        public async Task Save() =>
            await _context.SaveChangesAsync();
        

       
    }
}
