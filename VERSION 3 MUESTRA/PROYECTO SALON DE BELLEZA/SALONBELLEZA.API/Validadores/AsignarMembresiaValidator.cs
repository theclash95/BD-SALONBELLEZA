using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class AsignarMembresiaValidator : AbstractValidator<AsignarMembresiaRequest>
    {
        public AsignarMembresiaValidator()
        {
            RuleFor(x => x.IdCliente).GreaterThan(0).WithMessage("Debe seleccionar un cliente válido.");
            RuleFor(x => x.IdMembresia).GreaterThan(0).WithMessage("Debe seleccionar una membresía válida.");
        }
    }
}