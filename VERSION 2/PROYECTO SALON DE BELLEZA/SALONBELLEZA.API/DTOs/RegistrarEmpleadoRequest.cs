using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarEmpleadoRequest
    {
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [MaxLength(10)]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public string? TipoEmpleado { get; set; } // Ej: "PROFESIONAL", "ADMINISTRATIVO"
    }
}