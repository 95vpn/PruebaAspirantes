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

        public void Update(RolOpcion entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(RolOpcion entity)
        {
            throw new NotImplementedException();
        }



        public async Task Save() =>
            await _context.SaveChangesAsync();

        
    }
}
