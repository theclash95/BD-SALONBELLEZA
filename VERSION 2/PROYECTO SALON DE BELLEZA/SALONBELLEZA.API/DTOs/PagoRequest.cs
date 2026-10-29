using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class PagoRequest
{
    [Required]
    public int IdVenta { get; set; }

    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El método de pago es obligatorio.")]
    public string MetodoPago { get; set; } = string.Empty;

    public string? Referencia { get; set; }
}
