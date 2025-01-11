using System.Text.RegularExpressions;
using FluentValidation;
using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Validators
{
    public class PersonaInsertValidator :AbstractValidator<PersonaInsertDto>
    {
        public PersonaInsertValidator() 
        {
            RuleFor(x => x.Identificacion)
                .NotEmpty().WithMessage("Identificacion es obligatorio")
                .Length(10).WithMessage("Tiene que ser de 10 digitos")
                .Matches(@"\d{10}$").WithMessage("Debe contener números")
                .Must(DigitosSeguidos).WithMessage("No debe tener 4 numeros iguales");
        }

        private bool DigitosSeguidos(string identificacion)
        {
            // Patrón para detectar 4 dígitos consecutivos iguales
            var pattern = @"(\d)\1{3}";
            return !Regex.IsMatch(identificacion, pattern);
        }
    }
}
