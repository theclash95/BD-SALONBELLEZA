using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ServicioActualizarValidator : AbstractValidator<ServicioActualizarRequest>
    {
        public ServicioActualizarValidator()
        {
            RuleFor(x => x.IdServicio).GreaterThan(0).WithMessage("El ID del servicio es inválido.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre del servicio es obligatorio.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}