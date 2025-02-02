using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Services
{
    public interface ILogoutService
    {
        Task<string> Logout(LogoutDto logoutDto);

    }
}
