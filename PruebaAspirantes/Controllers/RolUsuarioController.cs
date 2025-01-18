using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Services;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        private ICommonService<RolUsuarioDto, RolUsuarioInsertDto, RolUsuarioUpdateDto> _rolUsuarioService;

        public RolUsuarioController([FromKeyedServices("rolUsuarioService")]ICommonService<RolUsuarioDto, RolUsuarioInsertDto, RolUsuarioUpdateDto> rolUsuarioService)
        {
            _rolUsuarioService = rolUsuarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<RolUsuarioDto>> Get() =>
            await _rolUsuarioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RolUsuarioDto>> GetById(int id)
        {
            var rolUsuarioDto = await _rolUsuarioService.GetById(id);

            return rolUsuarioDto == null ? NotFound() : Ok(rolUsuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<RolUsuarioDto>> Add(RolUsuarioInsertDto rolUsuarioInsertDto)
        {
            var rolInsertDto = await _rolUsuarioService.Add(rolUsuarioInsertDto);

            return rolInsertDto == null ? NotFound() : Ok(rolInsertDto);
        }

    }
}
