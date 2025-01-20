using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Repository
{
    public class SessionRepository : IRepository<Session>
    {
        private StoreContext _context;

        public SessionRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> Get() =>
            await _context.Sessions.ToListAsync();

        public async Task<Session> GetById(int id) =>
            await _context.Sessions.FindAsync(id);

        public async Task Add(Session session) =>
            await _context.Sessions.AddAsync(session);

        public void Update(Session session)
        {
            _context.Sessions.Attach(session);
            _context.Sessions.Entry(session).State = EntityState.Modified;

        }

        public void Delete(Session session)
        {
            _context.Sessions.Remove(session);
        }

        

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
