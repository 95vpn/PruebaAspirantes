using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;

namespace PruebaAspirantes.Validators
{
    public class UsuarioInsertValidator : AbstractValidator<UsuarioInsertDto>
    {
        private StoreContext _context;
        public UsuarioInsertValidator(StoreContext context) 
        {
            _context = context;

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre usuario no debe estar vacio")
                .Matches("^[a-zA-Z0-9]+").WithMessage("El nombre de usuarios no debe contener signos")
                .Matches("[A-Z]").WithMessage("El nombre de usuario debe contener al menos una letra mayuscula")
                .Matches("[0-9]").WithMessage("El nombre de usuario debe contener al menos un número")
                .Length(8, 20).WithMessage("El nombre de usuario debe contener de 2 a 20 caracteres")
                .MustAsync(async (userName, cancellation) =>
                {
                    var exiteUserName = await _context.Usuarios.AnyAsync(u => u.UserName == userName);
                    return !exiteUserName;
                }).WithMessage("El nombre de usuario ya existe");

            RuleFor(x => x.Password)
                .MaximumLength(8).WithMessage("Debe contener al menos 8 dígitos")
                .Matches("[A-Z]").WithMessage("Debe tener al menos una letra mayuscula")
                .Matches("^[^\\s]+$").WithMessage("La contraseña no debe contener espacios.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("La contraseña debe contener al menos un signo (por ejemplo, !, @, #, $)."); ;
        }
    }
}
