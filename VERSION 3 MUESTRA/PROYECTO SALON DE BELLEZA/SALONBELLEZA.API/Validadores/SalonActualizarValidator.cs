using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class SalonActualizarValidator : AbstractValidator<ActualizarSalonRequest>
    {
        public SalonActualizarValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del salón es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("El email no tiene un formato válido.");
        }
    }
}