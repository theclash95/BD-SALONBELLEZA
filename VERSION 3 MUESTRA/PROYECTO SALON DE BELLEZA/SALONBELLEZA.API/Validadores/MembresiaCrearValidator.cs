using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class MembresiaCrearValidator : AbstractValidator<MembresiaRequest>
    {
        public MembresiaCrearValidator()
        {
            RuleFor(x => x.NombrePlan).NotEmpty().WithMessage("El plan es obligatorio.");
            RuleFor(x => x.Costo).GreaterThan(0).WithMessage("El costo debe ser mayor a 0.");
        }
    }
}