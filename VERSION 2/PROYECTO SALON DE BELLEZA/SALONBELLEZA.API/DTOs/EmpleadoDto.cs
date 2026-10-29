using System;

namespace SALONBELLEZA.API.DTOs
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string? Dni { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? TipoEmpleado { get; set; }
        public string? Estado { get; set; }
    }
}