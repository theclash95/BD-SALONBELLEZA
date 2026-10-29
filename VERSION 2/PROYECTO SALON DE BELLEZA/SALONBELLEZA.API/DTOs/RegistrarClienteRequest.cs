using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarClienteRequest
    {
        
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El DNI no puede superar los 10 caracteres.")]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [MaxLength(100, ErrorMessage = "El email no puede superar los 100 caracteres.")]
        public string? Email { get; set; }

        [MaxLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres.")]
        public string? Direccion { get; set; }

        public string? Categoria { get; set; }
    }
}