using FluentValidation;
using SALONBELLEZA.API.DTOs;

namespace SALONBELLEZA.API.Validadores
{
    public class ClienteActualizarValidator : AbstractValidator<ClienteActualizarRequest>
    {
        public ClienteActualizarValidator()
        {
            RuleFor(x => x.IdCliente).GreaterThan(0).WithMessage("El ID del cliente es inválido.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio.");
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("El formato del email no es válido.");
        }
    }
}