using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
        private ILogoutService _logoutService;
        private IFilterUsuarioService _filterUsuarioService;
        

        public UsuarioController(
            IValidator<UsuarioInsertDto> usuarioInsertValidator,
            IValidator<UsuarioUpdateDto> usuarioUpdateValidator,
            [FromKeyedServices("usuarioService")] ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto> usuarioService,
            ILoginService loginService,
            ILogoutService logoutService,
            IFilterUsuarioService filterUsuarioService)
        {
            
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
            _usuarioService = usuarioService;
            _loginService = loginService;
            _logoutService = logoutService; ;
            _filterUsuarioService = filterUsuarioService;
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
        public async Task<IActionResult> Auntenticar([FromBody] LoginDto loginDto)
        {
            var loginTokenDto = await _loginService.Auth(loginDto);

            if (loginTokenDto == null)
                BadRequest();

            return Ok(loginTokenDto);
        }

        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto logoutDto)
        {
            var logout = await _logoutService.Logout(logoutDto);

            return Ok(logout);

        }

        [Authorize(Policy = "AdminSessiones")]
        [HttpGet("usuario/{idUsuario}")]
        
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSesionesUsuario(int idUsuario)
        {
            var token  = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var sesiones = await _filterUsuarioService.GetSesionesUsuario(idUsuario, token);

            return Ok(sesiones);
        }

    }
}
