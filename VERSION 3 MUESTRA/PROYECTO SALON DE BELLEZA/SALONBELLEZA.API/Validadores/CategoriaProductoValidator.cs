using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class CategoriaProductoValidator : AbstractValidator<CategoriaProductoRequest>
    {
        public CategoriaProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
        }
    }
}