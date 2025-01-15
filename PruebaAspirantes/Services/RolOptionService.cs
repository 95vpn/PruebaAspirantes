using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class RolOptionService : ICommonService<RolOptionDto, RolOptionInsertDto, RolOptionUpdateDto>
    {
        private IRepository<RolOpcion> _rolOptionRepository;

        public RolOptionService(IRepository<RolOpcion> rolOptionRepository)
        {
            _rolOptionRepository = rolOptionRepository;
        }

        public async Task<IEnumerable<RolOptionDto>> Get()
        {
            var rolOpciones = await _rolOptionRepository.Get();

            return rolOpciones.Select(ro => new RolOptionDto()
            {
                IdOpcion = ro.IdOpcion,
                NameOption = ro.NameOption,
            });
        }

        public async Task<RolOptionDto> GetById(int id)
        {
            var rolOption = await _rolOptionRepository.GetById(id);

            if (rolOption != null)
            {
                var rolOptionDto = new RolOptionDto()
                {
                    IdOpcion = rolOption.IdOpcion,
                    NameOption = rolOption.NameOption,
                };
                return rolOptionDto;
            }

            return null;
        }

        public async Task<RolOptionDto> Add(RolOptionInsertDto rolOptionInsertDto)
        {
            var rolOption = new RolOpcion()
            {
                NameOption = rolOptionInsertDto.NameOption,
            };

            await _rolOptionRepository.Add(rolOption);
            await _rolOptionRepository.Save();

            var rolOptionDto = new RolOptionDto()
            {
                IdOpcion = rolOption.IdOpcion,
                NameOption = rolOption.NameOption,
            };

            return rolOptionDto;
        }

        public async Task<RolOptionDto> Update(int id, RolOptionUpdateDto rolOptionUpdateDto)
        {
            var rolOption = await  _rolOptionRepository.GetById(id);
            if (rolOption != null)
            {
                rolOption.NameOption = rolOptionUpdateDto.NameOption;

                _rolOptionRepository.Update(rolOption);
                await _rolOptionRepository.Save();

                var rolOptionDto = new RolOptionDto()
                {
                    IdOpcion = rolOption.IdOpcion,
                    NameOption = rolOption.NameOption,
                };

                return rolOptionDto;

            }
            return null;
        }

        public async Task<RolOptionDto> Delete(int id)
        {
            var rolOption = await _rolOptionRepository.GetById(id);

            if (rolOption != null)
            {
                var rolOptionDto = new RolOptionDto()
                {
                    IdOpcion = rolOption.IdOpcion,
                    NameOption = rolOption.NameOption,
                };

                _rolOptionRepository.Delete(rolOption);
                await _rolOptionRepository.Save();

                return rolOptionDto;
            }
            return null;
        }

        

        
    }
}
