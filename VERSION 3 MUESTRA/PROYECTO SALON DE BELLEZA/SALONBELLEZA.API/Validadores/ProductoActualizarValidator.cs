using FluentValidation;
using global::SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ProductoActualizarValidator : AbstractValidator<ProductoActualizarRequest>
    {
        public ProductoActualizarValidator()
        {
            RuleFor(x => x.IdProducto).GreaterThan(0);
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PrecioVenta).GreaterThan(0);
        }
    }
}