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
        public  LoginTokenDto Auth(LoginDto loginDto)
        {

            
            string EncryptedPassword = Encrypt.GetSHA256(loginDto.Password);

            var usuario = _loginRepository.GetUsuarioByEmailAndPassword(loginDto.Email, EncryptedPassword);

            
            if (usuario == null)
            {
                return new LoginTokenDto
                {
                    
                    Email = null,
                    Token = "Credenciales inválidas"
                };
            }

            var usuarioEmail = _loginRepository.GetUsuarioByEmail(loginDto.Email);
            if (usuarioEmail != null)
            {
                usuarioEmail.IntentosFallidos = 0;
                usuarioEmail.IntentosFallidos++;
                if(usuarioEmail.IntentosFallidos > 3)
                {
                    usuarioEmail.Status = "bloqueado";
                    _loginRepository.Update(usuarioEmail);
                    _loginRepository.Save();
                }
            }

            var sessionActiva = _loginRepository.GetUsuarioById(usuario.IdUsuario);
            if (sessionActiva !=null && sessionActiva.SessionActive == "true" )
            {
                return new LoginTokenDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Token = "Ya tienes una session activa"
                };
            }

            usuario.SessionActive = "true"; 
            _loginRepository.Update(usuario); 
            _loginRepository.Save();


            var nuevaSession = new Session
            {
                FechaIngreso = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                FechaCierre = null,
                IdUsuario = usuario.IdUsuario,
            };

            _loginRepository.Add(nuevaSession);
            _loginRepository.Save();


            return new LoginTokenDto
            {
                IdUsuario = usuario.IdUsuario,
                Email = usuario.Email,
                Token = GetToken(usuario) 
            };

        }

        public void Logout(int userId)
        {
            var session = _loginRepository.SessionActiva(userId);

            if (session != null)
            {
                session.FechaCierre = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                _loginRepository.Update(session);
                _loginRepository.Save();

            }

            var usuario = _loginRepository.GetUsuarioById(userId);
            if (usuario != null)
            {
                usuario.SessionActive = "false";
                _loginRepository.Update(usuario);
                _loginRepository.Save();
            }
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSetting.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
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