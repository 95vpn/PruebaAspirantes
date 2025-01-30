using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Services
{
    public interface ILoginService
    {
        LoginTokenDto Auth(LoginDto loginDto);

        void Logout(int userId);
    }
}
