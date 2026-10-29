using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class VentaRequest
{
    [Required(ErrorMessage = "El turno es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un turno válido.")]
    public int IdTurno { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser un número negativo.")]
    public decimal Descuento { get; set; }

    public string? Observacion { get; set; }
}