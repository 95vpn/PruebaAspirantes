using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class FilterUsuarioRepository : IFilterUsuarioRepository
    {
        private StoreContext _context;

        public FilterUsuarioRepository(StoreContext context)
        {
            _context = context;
        }

       

        public async Task<IEnumerable<Session?>> GetSessions() =>
            await _context.Sessions.ToListAsync();


        public async Task<IEnumerable<Rol?>> GetRol() =>
            await _context.Roles.ToListAsync();

        
        
    }
}

