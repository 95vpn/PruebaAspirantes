using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Services;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<UsuarioInsertDto> _usuarioInsertValidator;
        private IValidator<UsuarioUpdateDto> _usuarioUpdateValidator;
        private ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> _usuarioService;
        

        public UsuarioController(StoreContext context,
            IValidator<UsuarioInsertDto> usuarioInsertValidator,
            IValidator<UsuarioUpdateDto> usuarioUpdateValidator,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> usuarioService)
        {
            _context = context;
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioDto>> Get() =>
            await _usuarioService.Get();
        
            

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            var usuarioDto = await _usuarioService.GetById(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {

            var validationResult = await _usuarioInsertValidator.ValidateAsync(usuarioInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var existeUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdPersona == usuarioInsertDto.IdPersona);
            if (existeUser != null)
            {
                return BadRequest($"La persona con IdPersona {usuarioInsertDto.IdPersona} ya tiene usuario asociado");
            }

            var persona = await _context.Personas.FindAsync(usuarioInsertDto.IdPersona);
            if (persona == null)
            {
                return NotFound("La persona asociada no existe");
            }

            var usuarioDto = await _usuarioService.Add(usuarioInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = usuarioDto.IdPersona }, usuarioDto);

           

           
        }
        

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var validationResult = await _usuarioUpdateValidator.ValidateAsync(usuarioUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            if (usuario.IdPersona != usuarioUpdateDto.IdPersona)
            {
                return BadRequest("No se puede cambiar el IdPersona asociado");
            }
            usuario.IdUsuario = usuarioUpdateDto.IdUsuario;
            usuario.UserName = usuarioUpdateDto.UserName;
            usuario.Password = usuarioUpdateDto.Password;
            usuario.IdPersona = usuarioUpdateDto.IdPersona;

            

            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                UserName = usuario.UserName,
                Password = usuario.Password,
                Email = usuario.Email,
                IdPersona = usuario.IdPersona,
            };

            return Ok(usuarioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
