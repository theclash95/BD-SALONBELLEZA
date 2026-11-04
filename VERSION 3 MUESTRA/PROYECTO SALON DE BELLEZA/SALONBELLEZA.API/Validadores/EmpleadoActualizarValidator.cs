using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class EmpleadoActualizarValidator : AbstractValidator<EmpleadoActualizarRequest>
    {
        public EmpleadoActualizarValidator()
        {
            RuleFor(x => x.IdEmpleado).GreaterThan(0);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50).WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(50).WithMessage("El apellido es obligatorio.");
            RuleFor(x => x.Dni).ValidarDni().When(x => !string.IsNullOrEmpty(x.Dni));
            RuleFor(x => x.Email).ValidarEmail().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}