using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Services;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolRolOpcionController : ControllerBase
    {
        private ICommonService<RolRolOpcionDto, RolRolOpcionInsertDto, RolRolOpcionUpdateDto> _rolRolOpcionService;

        public RolRolOpcionController([FromKeyedServices("rolRolOpcionService")] ICommonService<RolRolOpcionDto, RolRolOpcionInsertDto, RolRolOpcionUpdateDto> rolRolOpcionService)
        {
            _rolRolOpcionService = rolRolOpcionService;
        }

        [HttpGet]
        public async Task<IEnumerable<RolRolOpcionDto>> Get() =>
            await _rolRolOpcionService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RolRolOpcionDto>> GetById(int id)
        {
            var rolRolOpcionDto = await _rolRolOpcionService.GetById(id);

            return rolRolOpcionDto == null ? NotFound() : Ok(rolRolOpcionDto);

        }


        [HttpPost]

        public async Task<ActionResult<RolRolOpcionDto>> Add(RolRolOpcionInsertDto rolRolOpcionInsertDto)
        {
            var rolRolOpcionDto = await _rolRolOpcionService.Add(rolRolOpcionInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = rolRolOpcionDto.IdRol, idOption = rolRolOpcionDto.IdOption }, rolRolOpcionDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<RolRolOpcionDto>> Update(int id, RolRolOpcionUpdateDto rolRolOpcionUpdateDto)
        {
            var rolRolOptionDto = await _rolRolOpcionService.Update(id, rolRolOpcionUpdateDto);

            return rolRolOptionDto == null ? NotFound() : Ok( rolRolOptionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rolRolOpcionDto = await _rolRolOpcionService.Delete(id);

            return rolRolOpcionDto == null ? NotFound() : Ok(rolRolOpcionDto);
        }

    }
}
