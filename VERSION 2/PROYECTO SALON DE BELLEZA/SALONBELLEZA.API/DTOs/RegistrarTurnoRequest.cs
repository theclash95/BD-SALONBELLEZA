using System;
using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarTurnoRequest
    {
        [Required] public int IdCliente { get; set; }
        [Required] public int IdEmpleado { get; set; }
        [Required] public DateTime Fecha { get; set; }
        [Required] public TimeSpan Hora { get; set; }
        public string? Observacion { get; set; }
    }
}