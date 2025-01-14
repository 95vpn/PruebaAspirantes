using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;
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
        public async Task<ActionResult<RolOptionDto>> GetById(int Id)
        {
            var rolOptionDto = await _rolOptionService.GetById(Id);

            return rolOptionDto == null ? NotFound() : Ok(rolOptionDto);
        }

        [HttpPost]
        public async Task<ActionResult<RolOptionDto>> Add(RolOptionInsertDto rolOptionInsertDto)
        {
            var rolOptionDto = await _rolOptionService.Add(rolOptionInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = rolOptionDto.IdOpcion }, rolOptionDto);
        }
    }
}
