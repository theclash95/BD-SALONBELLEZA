using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarProveedorRequest
    {
        [Required(ErrorMessage = "La razón social es obligatoria.")]
        public string RazonSocial { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Cuit { get; set; }

        public string? Telefono { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Direccion { get; set; }
    }
}