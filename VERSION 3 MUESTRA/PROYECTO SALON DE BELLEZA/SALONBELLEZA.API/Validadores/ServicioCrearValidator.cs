using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ServicioCrearValidator : AbstractValidator<ServicioRequest>
    {
        public ServicioCrearValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100).WithMessage("El nombre del servicio es obligatorio.");
            RuleFor(x => x.IdCategoriaServicio).GreaterThan(0).WithMessage("Debe seleccionar una categoría.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}