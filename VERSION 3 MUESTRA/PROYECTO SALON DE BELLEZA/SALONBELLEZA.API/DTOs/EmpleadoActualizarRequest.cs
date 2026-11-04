using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class EmpleadoActualizarRequest
    {
        [Required] public int IdEmpleado { get; set; }
        [Required] public string Dni { get; set; } = string.Empty;
        [Required] public string Nombre { get; set; } = string.Empty;
        [Required] public string Apellido { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        [EmailAddress] public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? TipoEmpleado { get; set; }
    }
}