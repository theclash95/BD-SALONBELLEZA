using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class EmpleadoCrearValidator : AbstractValidator<EmpleadoRequest>
    {
        public EmpleadoCrearValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(50).WithMessage("El nombre es obligatorio (máx 50).");
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(50).WithMessage("El apellido es obligatorio (máx 50).");
            RuleFor(x => x.Dni).ValidarDni().When(x => !string.IsNullOrEmpty(x.Dni));
            RuleFor(x => x.Email).ValidarEmail().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Telefono).ValidarTelefono().When(x => !string.IsNullOrEmpty(x.Telefono));
            RuleFor(x => x.TipoEmpleado).NotEmpty().WithMessage("El tipo de empleado es obligatorio.");
        }
    }
}