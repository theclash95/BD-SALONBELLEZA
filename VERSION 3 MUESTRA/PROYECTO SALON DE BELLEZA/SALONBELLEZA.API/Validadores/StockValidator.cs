using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class StockValidator : AbstractValidator<StockRequest>
    {
        public StockValidator()
        {
            RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
            RuleFor(x => x.TipoMovimiento).Must(t => t == "INGRESO" || t == "EGRESO")
                .WithMessage("Debe ser INGRESO o EGRESO.");
        }
    }
}