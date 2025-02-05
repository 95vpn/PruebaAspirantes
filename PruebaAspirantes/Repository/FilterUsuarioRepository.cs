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

        public async Task<Usuario?> GetIdUsuario(int idUsuario)
        {
            return await _context.Usuarios.FindAsync(idUsuario);
        }

        public async Task<IEnumerable<Session>> GetSesionesIdUsuario(int idUsuario)
        {
            
            return await _context.Sessions.Where(s => s.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioRoles(int idUsuario)
        {
            return await _context.Usuarios.Include(u => u.RolUsuarios).ThenInclude(ru => ru.Rol).FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
        }
    }
}

