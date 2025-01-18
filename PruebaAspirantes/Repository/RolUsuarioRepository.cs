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

        public void Delete(RolUsuario entity)
        {
            throw new NotImplementedException();
        }

        

        public async Task Save() =>
            await _context.SaveChangesAsync();
        

        public void Update(RolUsuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
