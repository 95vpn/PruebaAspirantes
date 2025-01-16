using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Services;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolOptionController : ControllerBase
    {
        private ICommonService<RolOptionDto, RolOptionInsertDto, RolOptionUpdateDto> _rolOptionService;

        public RolOptionController([FromKeyedServices("rolOptionService")]ICommonService<RolOptionDto, RolOptionInsertDto, RolOptionUpdateDto> rolOptionService)
        {
            _rolOptionService = rolOptionService;
        }

        [HttpGet]
        public async Task<IEnumerable<RolOptionDto>> Get() =>
            await _rolOptionService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RolOptionDto>> GetById(int id)
        {
            var rolOptionDto = await _rolOptionService.GetById(id);

            return rolOptionDto == null ? NotFound() : Ok(rolOptionDto);
        }

        [HttpPost]
        public async Task<ActionResult<RolOptionDto>> Add(RolOptionInsertDto rolOptionInsertDto)
        {
            var rolOptionDto = await _rolOptionService.Add(rolOptionInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = rolOptionDto.IdOpcion }, rolOptionDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RolOptionDto>> Update(int id, RolOptionUpdateDto rolOptionUpdateDto)
        {
            var rolOptionDto = await _rolOptionService.Update(id, rolOptionUpdateDto);

            return rolOptionDto == null ? NotFound() : Ok(rolOptionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rolOptionDto = await _rolOptionService.Delete(id);

            return rolOptionDto == null ? NotFound() : Ok(rolOptionDto);
        }
    }
}
