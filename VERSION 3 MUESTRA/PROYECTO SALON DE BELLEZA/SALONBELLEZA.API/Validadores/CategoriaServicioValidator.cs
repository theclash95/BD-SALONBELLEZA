using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class CategoriaServicioValidator : AbstractValidator<CategoriaServicioRequest>
    {
        public CategoriaServicioValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
        }
    }
}