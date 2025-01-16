using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class RolRepository : IRepository<Rol>
    {
        private StoreContext _context;

        public RolRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> Get() =>
            await _context.Roles.ToListAsync();

        public async Task<Rol> GetById(int id) => 
            await _context.Roles.FindAsync(id);

        /*
        public async Task<Rol> GetById(params object[] keyValues) =>
            await _context.Roles.FindAsync(keyValues);
        */

        public async Task Add(Rol rol) =>
            await _context.Roles.AddAsync(rol);


        public void Update(Rol rol)
        {
            _context.Roles.Attach(rol);
            _context.Roles.Entry(rol).State = EntityState.Modified;
        }


        public void Delete(Rol rol) =>
            _context.Roles.Remove(rol);


        
        public async Task Save() =>
            await _context.SaveChangesAsync();

        
    }
}
