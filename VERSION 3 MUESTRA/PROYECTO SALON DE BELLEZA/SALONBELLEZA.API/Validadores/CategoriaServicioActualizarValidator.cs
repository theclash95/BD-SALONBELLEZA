using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class CategoriaServicioActualizarValidator : AbstractValidator<CategoriaServicioActualizarRequest>
    {
        public CategoriaServicioActualizarValidator()
        {
            RuleFor(x => x.IdCategoriaServicio).GreaterThan(0).WithMessage("El ID es inválido.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");
        }
    }
}