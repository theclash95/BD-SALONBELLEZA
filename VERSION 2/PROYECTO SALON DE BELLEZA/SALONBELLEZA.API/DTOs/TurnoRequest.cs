using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class TurnoRequest
{
    [Required(ErrorMessage = "El cliente es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El empleado es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido.")]
    public int IdEmpleado { get; set; }

    [Required(ErrorMessage = "La fecha del turno es obligatoria.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "La hora del turno es obligatoria.")]
    public TimeSpan Hora { get; set; }

    public string? Observacion { get; set; }
}