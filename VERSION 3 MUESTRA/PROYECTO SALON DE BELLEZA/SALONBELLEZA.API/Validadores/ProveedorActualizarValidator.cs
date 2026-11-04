using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ProveedorActualizarValidator : AbstractValidator<ProveedorActualizarRequest>
    {
        public ProveedorActualizarValidator()
        {
            RuleFor(x => x.IdProveedor).GreaterThan(0);
            RuleFor(x => x.RazonSocial).NotEmpty().MaximumLength(100).WithMessage("La razón social es obligatoria.");
        }
    }
}