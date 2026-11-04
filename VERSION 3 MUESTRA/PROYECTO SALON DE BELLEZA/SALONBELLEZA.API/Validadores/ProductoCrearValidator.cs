using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ProductoCrearValidator : AbstractValidator<ProductoRequest>
    {
        public ProductoCrearValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100).WithMessage("El nombre del producto es obligatorio.");
            RuleFor(x => x.IdCategoriaProducto).GreaterThan(0).WithMessage("Debe seleccionar una categoría.");
            RuleFor(x => x.PrecioVenta).GreaterThan(0).WithMessage("El precio de venta debe ser mayor a 0.");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }
    }
}