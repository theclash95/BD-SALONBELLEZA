using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ProveedorCrearValidator : AbstractValidator<ProveedorRequest>
    {
        public ProveedorCrearValidator()
        {
            RuleFor(x => x.RazonSocial).NotEmpty().MaximumLength(100).WithMessage("La razón social es obligatoria.");
            RuleFor(x => x.Cuit).MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Cuit));
            RuleFor(x => x.Email).ValidarEmail().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}