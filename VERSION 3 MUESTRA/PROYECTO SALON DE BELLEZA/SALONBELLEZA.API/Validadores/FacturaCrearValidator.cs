using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class FacturaCrearValidator : AbstractValidator<FacturaRequest>
    {
        public FacturaCrearValidator()
        {
            RuleFor(x => x.IdVenta).GreaterThan(0).WithMessage("El ID de la venta es obligatorio.");
            RuleFor(x => x.NumeroFactura).NotEmpty().WithMessage("El número de factura es obligatorio.");
            RuleFor(x => x.TipoComprobante).NotEmpty().WithMessage("El tipo de comprobante es obligatorio.");
        }
    }
}