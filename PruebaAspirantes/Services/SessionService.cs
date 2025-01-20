using Microsoft.AspNetCore.Http;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class SessionService : ICommonService<SessionDto, SessionInsertDto, SessionUpdateDto>
    {
        private IRepository<Session> _sessionRepository;

        public SessionService(IRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IEnumerable<SessionDto>> Get()
        {
            var session = await _sessionRepository.Get();

            return session.Select(s => new SessionDto()
            {
                IdSession = s.IdSession,
                FechaIngreso = s.FechaIngreso,
                FechaCierre = s.FechaCierre,
                IdUsuario = s.IdUsuario,

            });

        }

        public async Task<SessionDto> GetById(int id)
        {
            var session = await _sessionRepository.GetById(id);

            if (session != null)
            {
                var sessionDto = new SessionDto()
                {
                    IdSession = session.IdSession,
                    FechaIngreso = session.FechaIngreso,
                    FechaCierre = session.FechaCierre,
                    IdUsuario = session.IdUsuario,
                };
                return sessionDto;
            }
            return null;
        }

        public async Task<SessionDto> Add(SessionInsertDto sessionInsertDto)
        {
            var session = new Session()
            {
                FechaIngreso = sessionInsertDto.FechaIngreso,
                FechaCierre = sessionInsertDto.FechaCierre,
                IdUsuario = sessionInsertDto.IdUsuario,
            };

            await _sessionRepository.Add(session);
            await _sessionRepository.Save();

            var sessionDto = new SessionDto()
            {
                IdSession = session.IdSession,
                FechaIngreso = session.FechaIngreso,
                FechaCierre = session.FechaCierre,
                IdUsuario = session.IdUsuario,
            };

            return sessionDto;
        }

        public async Task<SessionDto> Update(int id, SessionUpdateDto sessionUpdateDto)
        {
            var session = await _sessionRepository.GetById(id);

            if (session != null)
            {
                session.FechaIngreso = sessionUpdateDto.FechaIngreso;
                session.FechaCierre = sessionUpdateDto.FechaCierre;
                session.IdUsuario = sessionUpdateDto.IdUsuario;

                _sessionRepository.Update(session);
                await _sessionRepository.Save();

                var sessionDto = new SessionDto()
                {
                    IdSession = session.IdSession,
                    FechaIngreso = session.FechaIngreso,
                    FechaCierre = session.FechaCierre,
                    IdUsuario = session.IdUsuario,
                };

                return sessionDto;
            }
            return null;
        }

        public async Task<SessionDto> Delete(int id)
        {
            var session = await _sessionRepository.GetById(id);

            if (session != null)
            {
                var sessionDto = new SessionDto()
                {
                    IdSession = session.IdSession,
                    FechaIngreso = session.FechaIngreso,
                    FechaCierre = session.FechaCierre,
                    IdUsuario = session.IdUsuario,
                };

                _sessionRepository.Delete(session);
                await _sessionRepository.Save();

                return sessionDto;
            }
            return null;
        }

       

       
    }
}
