using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Services
{
    public class RolService : ICommonService<RolDto, RolIsertDto, RolUpdateDto>
    {
        private StoreContext _context;

        public RolService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolDto>> Get() =>
            await _context.Roles.Select(r => new RolDto
            {
                IdRol = r.IdRol,
                RolName = r.RolName
            }).ToListAsync();

        public async Task<RolDto> GetById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol != null)
            {
                var rolDto = new RolDto
                {
                    IdRol = rol.IdRol,
                    RolName = rol.RolName
                };
                return rolDto;
            }
            return null;
        }

        public async Task<RolDto> Add(RolIsertDto rolInsertDto)
        {
            var rol = new Rol()
            {
                RolName = rolInsertDto.RolName
            };

            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();

            var rolDto = new RolDto
            {
                IdRol = rol.IdRol,
                RolName = rol.RolName
            };
            return rolDto;
        }


        public async Task<RolDto> Update(int id, RolUpdateDto rolUpdateDto)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                rol.RolName = rolUpdateDto.RolName;

                await _context.SaveChangesAsync();

                var rolDto = new RolDto
                {
                    IdRol = rol.IdRol,
                    RolName = rol.RolName
                };

                return rolDto;

            }
            return null;
        }


        public async Task<RolDto> Delete(int id)
        {
            throw new NotImplementedException();
        }




    }
}
