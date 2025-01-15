using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class RolOptionRepository : IRepository<RolOpcion>
    {
        private StoreContext _context;

        public RolOptionRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolOpcion>> Get() =>
            await _context.RolOpciones.ToListAsync();


        public async Task<RolOpcion> GetById(int id) =>
            await _context.RolOpciones.FindAsync(id);


        public async Task Add(RolOpcion rolOpcion) =>
            await _context.RolOpciones.AddAsync(rolOpcion);

        public void Update(RolOpcion rolOption)
        {
            _context.RolOpciones.Attach(rolOption);
            _context.RolOpciones.Entry(rolOption).State = EntityState.Modified;
        }

        public void Delete(RolOpcion rolOption)
        {
            _context.RolOpciones.Remove(rolOption);
        }



        public async Task Save() =>
            await _context.SaveChangesAsync();

        
    }
}
