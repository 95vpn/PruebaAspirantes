using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Services
{
    public class UsuarioService : ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto>
    {
        private StoreContext _context;

        public UsuarioService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDto>> Get() =>
            await _context.Usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                UserName = u.UserName,
                Password = u.Password,
                Email = u.Email,
                IdPersona = u.IdPersona,
            }).ToListAsync();

        public async Task<UsuarioDto> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName,
                    Password = usuario.Password,
                    Email = usuario.Email,
                    IdPersona = usuario.IdPersona,
                };
                return usuarioDto;
            }
            return null;
        }

        public async Task<UsuarioDto> Add(UsuarioInsertDto usuarioInsertDto)
        {
            
            var persona = await _context.Personas.FindAsync(usuarioInsertDto.IdPersona);

            string emailGenerado = GenerarEmail(persona);
            string emailUnico = await UnicoEmail(emailGenerado);

            var usuario = new Usuario()
            {
                UserName = usuarioInsertDto.UserName,
                Password = usuarioInsertDto.Password,
                IdPersona = usuarioInsertDto.IdPersona,
                Email = emailUnico,
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                UserName = usuario.UserName,
                Password = usuario.Password,
                IdPersona = usuario.IdPersona,
                Email = usuario.Email
            };

            return usuarioDto;


        }
        private string GenerarEmail(Persona persona)
        {
            string nombreLetraInicial = string.IsNullOrEmpty(persona.Names) ? "" : persona.Names.Split(' ')[0].Substring(0, 1).ToLower();
            string primerApellido = string.IsNullOrEmpty(persona.LastNames) ? "" : persona.LastNames.Split(' ')[0].ToLower();

            return $"{nombreLetraInicial}{primerApellido}@mail.com";
        }

        private async Task<string> UnicoEmail(string emailgenerado)
        {
            string email = emailgenerado;
            int counter = 0;

            while (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                counter++;
                email = $"{emailgenerado.Split('@')[0]}{counter}@mail.com";

            }
            return email;
        }

        public async Task<UsuarioDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> Update(int id, UsuarioUpdateDto personaUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
