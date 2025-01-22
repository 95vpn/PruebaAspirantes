using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;
using PruebaAspirantes.Tools;

namespace PruebaAspirantes.Services
{
    public class LoginService : ILoginService
    {
        
        private ILoginRepository _loginRepository;
        private AppSetting _appSetting;

        public LoginService(ILoginRepository loginRepository,
            IOptions<AppSetting> appSetting)
        {
            _loginRepository = loginRepository;
            _appSetting = appSetting.Value;
        }
        public LoginTokenDto Auth(LoginDto loginDto)
        {
            
            
            string EncryptedPassword = Encrypt.GetSHA256(loginDto.Password);

            var usuario = _loginRepository.GetUsuarioByEmailAndPassword(loginDto.Email, EncryptedPassword);
            /*
            if (usuario != null)
            {
                
            }
            return null;
            */
            return new LoginTokenDto
            {
                Email = loginDto.Email,
                Token = GetToken(loginDto)
            };

        }

        private string GetToken(LoginDto loginDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSetting.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginDto.Email),

                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
    }
}
