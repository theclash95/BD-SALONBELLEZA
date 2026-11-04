using FluentValidation;

namespace SALONBELLEZA.API.Validadores
{
    public static class ValidacionesComunes
    {
        // 1. Validar Email (acepta string o string?)
        public static IRuleBuilderOptions<T, string> ValidarEmail<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.EmailAddress().WithMessage("El formato del email no es válido.");
        }

        // 2. Validar DNI
        public static IRuleBuilderOptions<T, string> ValidarDni<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"^\d{7,8}$").WithMessage("El DNI debe contener entre 7 y 8 dígitos.");
        }

        // 3. Validar Teléfono
        public static IRuleBuilderOptions<T, string> ValidarTelefono<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"^\+?\d{7,15}$").WithMessage("El teléfono debe contener entre 7 y 15 dígitos (puede incluir '+').");
        }

        // 4. Validar CUIT
        public static IRuleBuilderOptions<T, string> ValidarCuit<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"^\d{2}-?\d{8}-?\d{1}$").WithMessage("El CUIT debe tener un formato válido (ej: 30-12345678-9).");
        }

        // 5. Validar Monto Positivo
        public static IRuleBuilderOptions<T, decimal> ValidarMontoPositivo<T>(this IRuleBuilderInitial<T, decimal> ruleBuilder)
        {
            return ruleBuilder.GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");
        }

        // 6. Validar Longitud Máxima
        public static IRuleBuilderOptions<T, string> ValidarLongitudMaxima<T>(this IRuleBuilderInitial<T, string> ruleBuilder, int max)
        {
            return ruleBuilder.MaximumLength(max).WithMessage($"El campo no puede superar los {max} caracteres.");
        }
    }
}