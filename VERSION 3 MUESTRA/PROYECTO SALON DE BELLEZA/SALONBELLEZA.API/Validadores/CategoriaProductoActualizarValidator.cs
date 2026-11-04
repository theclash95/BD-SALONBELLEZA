using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class CategoriaProductoActualizarValidator : AbstractValidator<CategoriaProductoActualizarRequest>
    {
        public CategoriaProductoActualizarValidator()
        {
            RuleFor(x => x.IdCategoriaProducto).GreaterThan(0).WithMessage("El ID es inválido.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");
        }
    }
}