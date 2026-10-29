using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class ProveedorActualizarRequest
    {
        [Required]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria.")]
        public string RazonSocial { get; set; } = string.Empty;

        public string? Cuit { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
    }
}