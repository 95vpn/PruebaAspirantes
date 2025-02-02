using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Services
{
    public interface ILoginService
    {
        Task<LoginTokenDto> Auth(LoginDto loginDto);

        
    }
}
