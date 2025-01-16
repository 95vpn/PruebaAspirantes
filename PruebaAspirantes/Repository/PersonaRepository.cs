using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class PersonaRepository : IRepository<Persona>
    {
        private StoreContext _context;

        public PersonaRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Persona>> Get() => 
            await _context.Personas.ToListAsync();

        public async Task<Persona> GetById(int id) =>
            await _context.Personas.FindAsync(id);

        /*
        public async Task<Persona> GetById(params object[] keyValues) =>
            await _context.Personas.FindAsync(keyValues);
        */

        public async Task Add(Persona persona) =>
            await _context.Personas.AddAsync(persona);

        public void Update(Persona persona)
        {
            _context.Personas.Attach(persona);
            _context.Personas.Entry(persona).State = EntityState.Modified;
        }

        public void Delete(Persona persona) =>
            _context.Personas.Remove(persona);
        

        public async Task Save() =>
            await _context.SaveChangesAsync();

        
    }
}
