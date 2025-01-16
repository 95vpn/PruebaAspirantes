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
    public class RolController : ControllerBase
    {
        
        private ICommonService<RolDto, RolIsertDto, RolUpdateDto> _rolService;

        public RolController(
            [FromKeyedServices("rolService")] ICommonService<RolDto, RolIsertDto, RolUpdateDto> rolService)
        {
            
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IEnumerable<RolDto>> Get() =>
            await _rolService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetById(int id)
        {
            var rolDto = await _rolService.GetById(id);

            return rolDto == null ? NotFound() : Ok(rolDto);

        }

        [HttpPost]
        public async Task<ActionResult<RolDto>> Add(RolIsertDto rolInsertDto)
        {
            var rolDto = await _rolService.Add(rolInsertDto);

            return CreatedAtAction(nameof(GetById), new { id =  rolDto.IdRol }, rolDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RolDto>> Update(int id, RolUpdateDto rolUpdateDto)
        {
            var rolDto = await _rolService.Update(id, rolUpdateDto);

            return rolDto == null ? NotFound() : Ok(rolDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rolDto = await _rolService.Delete(id);
            return rolDto == null ? NotFound() : Ok(rolDto);
        }
        

    }
}
