using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Services;

namespace PruebaAspirantes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ICommonService<SessionDto, SessionInsertDto, SessionUpdateDto> _sessionService;

        public SessionController([FromKeyedServices("sessionService")]ICommonService<SessionDto, SessionInsertDto, SessionUpdateDto> sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IEnumerable<SessionDto>> Get() =>
            await _sessionService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetById(int id)
        {
            var sessionDto = await _sessionService.GetById(id);

            return sessionDto == null ? NotFound() : Ok(sessionDto);
        }

        [HttpPost]
        public async Task<ActionResult<SessionDto>> Add(SessionInsertDto sessionInsertDto)
        {
            var sessionDto = await _sessionService.Add(sessionInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = sessionDto.IdSession }, sessionDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<SessionDto>> Update(int id, SessionUpdateDto sessionUpdateDto)
        {
            var sessionDto = await _sessionService.Update(id, sessionUpdateDto);

            return sessionDto == null ? NotFound() : Ok(sessionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sessionDto = await _sessionService.Delete(id);

            return sessionDto == null ? NotFound() : Ok(sessionDto);
        }

    }
}
