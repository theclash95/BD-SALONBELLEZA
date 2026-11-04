using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class PagoCrearValidator : AbstractValidator<PagoRequest>
    {
        public PagoCrearValidator()
        {
            RuleFor(x => x.IdVenta).GreaterThan(0).WithMessage("El ID de la venta es obligatorio.");
            RuleFor(x => x.Monto).GreaterThan(0).WithMessage("El monto debe ser mayor a 0.");
            RuleFor(x => x.MetodoPago).NotEmpty().WithMessage("El método de pago es obligatorio.");
        }
    }
}