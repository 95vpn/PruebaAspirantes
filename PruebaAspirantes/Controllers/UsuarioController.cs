using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private StoreContext _context;

        public UsuarioController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> Get() =>
        
            await _context.Usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                UserName = u.UserName,
                Password = u.Password,
                IdPersona = u.IdPersona,
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                UserName = usuario.UserName,
                Password = usuario.Password,
                IdPersona = usuario.IdPersona,
            };
            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var usuario = new Usuario()
            {
                UserName = usuarioInsertDto.UserName,
                Password = usuarioInsertDto.Password,
                IdPersona = usuarioInsertDto.IdPersona
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                UserName = usuario.UserName,
                Password = usuario.Password,
                IdPersona = usuario.IdPersona,
            };

            return CreatedAtAction(nameof(GetById), new { id = usuario.IdPersona }, usuarioDto);
        }
    }
}
