using Microsoft.EntityFrameworkCore;
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

        public async Task<Session?> GetSesionesUsuario(string token)
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token && s.FechaCierre == null);
        }
    }
}
