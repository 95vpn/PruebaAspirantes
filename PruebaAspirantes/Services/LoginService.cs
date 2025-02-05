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
        private IRepository<Usuario> _usuarioRepository;

        public LoginService(ILoginRepository loginRepository,
            IOptions<AppSetting> appSetting,
            IRepository<Usuario> usuarioRepository)
        {
            _loginRepository = loginRepository;
            _appSetting = appSetting.Value;
            _usuarioRepository = usuarioRepository;
        }
        public async  Task<LoginTokenDto> Auth(LoginDto loginDto)
        {

            
            string EncryptedPassword = Encrypt.GetSHA256(loginDto.Password);

            var usuario = await _loginRepository.GetUsuarioByEmailAndPassword(loginDto.Email, EncryptedPassword);

            
            if (usuario == null)
            {
                
                var usuarioEmail = await _loginRepository.GetUsuarioByEmail(loginDto.Email);
                if (usuarioEmail != null)
                {
                    
                    usuarioEmail.IntentosFallidos++;
                    Console.WriteLine($"Intentos Fallidos: {usuarioEmail.IntentosFallidos}");
                    if (usuarioEmail.IntentosFallidos >= 3)
                    {
                        usuarioEmail.Status = "bloqueado";
                        _loginRepository.Update(usuarioEmail);
                        await _loginRepository.Save();
                    }
                    
                }
                
                return new LoginTokenDto
                {
                    
                    Email = null,
                    Token = "Credenciales inválidas"
                };
            }
            
            if (usuario.Status == "bloqueado")
            {
                return new LoginTokenDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Token = "Tu cuenta ha sido bloqueada por demasiados intentos fallidos"
                };
            }

            

            var sessionActiva = await _loginRepository.GetUsuarioById(usuario.IdUsuario);
            if (sessionActiva != null && sessionActiva.SessionActive == "true" )
            {
                return new LoginTokenDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Token = "Ya tienes una session activa"
                };
            }

            usuario.IntentosFallidos = 0;
            
            usuario.SessionActive = "true"; 
            _loginRepository.Update(usuario); 
            await _loginRepository.Save();


            var nuevaSession = new Session
            {
                FechaIngreso = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                FechaCierre = null,
                IdUsuario = usuario.IdUsuario,
                Token = GetToken(usuario)
            };

            await _loginRepository.Add(nuevaSession);
            await _loginRepository.Save();


            var loginToken = new LoginTokenDto
            {
                IdUsuario = usuario.IdUsuario,
                Email = usuario.Email,
                Token = GetToken(usuario) 
            };
            return loginToken;

        }

        

        private  string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSetting.Secreto);

            var roles = usuario.RolUsuarios?.Select(ru => ru.Rol?.RolName).Where(nombre => !string.IsNullOrEmpty(nombre)).ToList();

            var rolesConcatenados = roles != null ? string.Join(",", roles) : string.Empty;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("RolName", rolesConcatenados)
            };
                    
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        

    }
}