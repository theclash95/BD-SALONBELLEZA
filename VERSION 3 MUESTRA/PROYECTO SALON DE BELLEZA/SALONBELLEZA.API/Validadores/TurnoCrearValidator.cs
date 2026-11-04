using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class TurnoCrearValidator : AbstractValidator<TurnoRequest>
    {
        public TurnoCrearValidator()
        {
            RuleFor(x => x.IdCliente).GreaterThan(0).WithMessage("Debe seleccionar un cliente.");
            RuleFor(x => x.IdEmpleado).GreaterThan(0).WithMessage("Debe seleccionar un empleado.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("La fecha es obligatoria.");
        }
    }
}