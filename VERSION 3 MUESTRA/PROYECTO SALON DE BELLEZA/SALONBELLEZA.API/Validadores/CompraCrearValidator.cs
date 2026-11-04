using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class CompraCrearValidator : AbstractValidator<CompraRequest>
    {
        public CompraCrearValidator()
        {
            RuleFor(x => x.IdProveedor).GreaterThan(0).WithMessage("Debe seleccionar un proveedor.");
            RuleFor(x => x.IdProducto).GreaterThan(0).WithMessage("Debe seleccionar un producto.");
            RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
            RuleFor(x => x.PrecioCosto).GreaterThan(0).WithMessage("El precio de costo debe ser mayor a 0.");
        }
    }
}