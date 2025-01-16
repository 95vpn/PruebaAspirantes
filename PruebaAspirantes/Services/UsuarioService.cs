using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;

namespace PruebaAspirantes.Services
{
    public class UsuarioService : ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto>
    {
        
        private IRepository<Usuario> _usuarioRepository;
        private IRepository<Persona> _personaRepository;

        public UsuarioService(
            IRepository<Usuario> usuarioRepository,
            IRepository<Persona> personaRepository)
        {
            
            _usuarioRepository = usuarioRepository;
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> Get()
        {
            var usuarios = await _usuarioRepository.Get();

            return usuarios.Select(u => new UsuarioDto()
            {
                IdUsuario = u.IdUsuario,
                UserName = u.UserName,
                Password = u.Password,
                Email = u.Email,
                IdPersona = u.IdPersona,
            });
        }

        public async Task<UsuarioDto> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName,
                    Password = usuario.Password,
                    Email = usuario.Email,
                    IdPersona = usuario.IdPersona,
                };

                return usuarioDto;
            }
            return null;
        }
        /*
        public async Task<UsuarioDto> GetById(params object[] keyValues)
        {
            var usuario = await _usuarioRepository.GetById(keyValues);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName,
                    Password = usuario.Password,
                    Email = usuario.Email,
                    IdPersona = usuario.IdPersona,
                };

                return usuarioDto;
            }
            return null;
        }
        */
        public async Task<UsuarioDto> Add(UsuarioInsertDto usuarioInsertDto)
        {
            
            var persona = await _personaRepository.GetById(usuarioInsertDto.IdPersona);

            if (persona == null)
            {
                throw new KeyNotFoundException("la persona asociada no existe");
            }

            var usuarios = await _usuarioRepository.Get();
            var existeUser =  usuarios.Any(u => u.IdPersona == usuarioInsertDto.IdPersona);
            if (existeUser)
            {
                throw new ($"La persona con IdPersona {usuarioInsertDto.IdPersona} ya tiene usuario asociado");
            }

            string emailGenerado = GenerarEmail(persona);
            string emailUnico = await UnicoEmail(emailGenerado);

            var usuario = new Usuario()
            {
                UserName = usuarioInsertDto.UserName,
                Password = usuarioInsertDto.Password,
                IdPersona = usuarioInsertDto.IdPersona,
                Email = emailUnico,
            };

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.Save();

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                UserName = usuario.UserName,
                Password = usuario.Password,
                IdPersona = usuario.IdPersona,
                Email = usuario.Email
            };

            return usuarioDto;

        }
        private string GenerarEmail(Persona persona)
        {
            string nombreLetraInicial = string.IsNullOrEmpty(persona.Names) ? "" : persona.Names.Split(' ')[0].Substring(0, 1).ToLower();
            string primerApellido = string.IsNullOrEmpty(persona.LastNames) ? "" : persona.LastNames.Split(' ')[0].ToLower();
            string inicialSegundoApellido = string.IsNullOrEmpty(persona.LastNames) ? "" : persona.LastNames.Split(' ')[1].Substring(0, 1).ToLower();
               

            return $"{nombreLetraInicial}{primerApellido}{inicialSegundoApellido}@mail.com";
        }

        private async Task<string> UnicoEmail(string emailgenerado)
        {
            string email = emailgenerado;
            int counter = 0;

            var usuarios = await _usuarioRepository.Get();
            while ( usuarios.Any(u => u.Email == email))
            {
                counter++;
                email = $"{emailgenerado.Split('@')[0]}{counter}@mail.com";

            }
            return email;
        }


        public async Task<UsuarioDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
            {
                return null; // Usuario no encontrado
            }

            // Validar que el IdPersona no cambie
            if (usuario.IdPersona != usuarioUpdateDto.IdPersona)
            {
                throw new InvalidOperationException("No se puede cambiar el IdPersona asociado.");
            }

            if (usuario != null)
            {
                usuario.IdUsuario = usuarioUpdateDto.IdUsuario;
                usuario.UserName = usuarioUpdateDto.UserName;
                usuario.Password = usuarioUpdateDto.Password;
                usuario.IdPersona = usuarioUpdateDto.IdPersona;

                _usuarioRepository.Update(usuario);
                await _usuarioRepository.Save();

                var usuarioDto = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName,
                    Password = usuario.Password,
                    Email = usuario.Email,
                    IdPersona = usuario.IdPersona,
                };

                return usuarioDto;
            }
            return null;
        }

        public async Task<UsuarioDto> Delete(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario != null)
            {
                var usuarioDto = new UsuarioDto
                {
                    IdUsuario = usuario.IdUsuario,
                    UserName = usuario.UserName,
                    Password = usuario.Password,
                    Email = usuario.Email,
                    IdPersona = usuario.IdPersona,
                };

                _usuarioRepository.Delete(usuario);
                await _usuarioRepository.Save();

                return usuarioDto;
            }
            return null;
        }

        
    }
}
