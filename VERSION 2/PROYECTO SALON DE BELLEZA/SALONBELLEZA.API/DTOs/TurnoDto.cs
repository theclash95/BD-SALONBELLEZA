using System;
namespace SALONBELLEZA.API.DTOs
{
    public class TurnoDto
    {
        public int IdTurno { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string? Estado { get; set; }
        public string? Observacion { get; set; }
    }
}