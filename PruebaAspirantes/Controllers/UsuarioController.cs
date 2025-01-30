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
        
        private IValidator<UsuarioInsertDto> _usuarioInsertValidator;
        private IValidator<UsuarioUpdateDto> _usuarioUpdateValidator;
        private ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> _usuarioService;
        private ILoginService _loginService;
        

        public UsuarioController(
            IValidator<UsuarioInsertDto> usuarioInsertValidator,
            IValidator<UsuarioUpdateDto> usuarioUpdateValidator,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> usuarioService,
            ILoginService loginService)
        {
            
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
            _usuarioService = usuarioService;
            _loginService = loginService;
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

            var usuarioDto = await _usuarioService.Update(id, usuarioUpdateDto);
   
            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuarioDto = await _usuarioService.Delete(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpPost("login")]
        public IActionResult Auntenticar([FromBody] LoginDto loginDto)
        {
            var loginTokenDto = _loginService.Auth(loginDto);

            if (loginTokenDto == null)
                BadRequest();

            return Ok(loginTokenDto);
        }

        [HttpPost("logout/{userId}")]
        public IActionResult Logout(int userId)
        {
            _loginService.Logout(userId);
            return Ok(new {message = "Session cerrada correctamente"});

        }

    }
}
