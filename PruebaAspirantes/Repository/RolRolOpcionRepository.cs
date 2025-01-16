using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class RolRolOpcionRepository : IRolRolOpcionRepository
    {
        private StoreContext _context;

        public RolRolOpcionRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolRolOpcion>> Get() =>
             await _context.RolRolOpciones.ToListAsync();

        public async Task<RolRolOpcion> GetById(int id) =>
            await _context.RolRolOpciones.FindAsync(id);
        /*
        public async Task<RolRolOpcion> GetById(params object[] keyValues) =>
            await _context.RolRolOpciones.FindAsync(keyValues);
        */

        public async Task Add(RolRolOpcion rolRolOpcion) =>
            await _context.RolRolOpciones.AddAsync(rolRolOpcion);

        public void Update(RolRolOpcion rolRolOpcion)
        {
            _context.RolRolOpciones.Attach(rolRolOpcion);
            _context.RolRolOpciones.Entry(rolRolOpcion).State = EntityState.Modified;
        }

        public void Delete(RolRolOpcion rolRolOpcion)
        {
            _context.RolRolOpciones.Remove(rolRolOpcion);
        }

        

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteRol(int idRol)
        {
            return await _context.Roles.AnyAsync(r => r.IdRol == idRol);
        }

        public async Task<bool> ExisteOpcion(int idOption)
        {
            return await _context.RolOpciones.AnyAsync(o => o.IdOpcion == idOption);
        }
    }
}
