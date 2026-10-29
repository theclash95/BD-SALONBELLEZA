using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class FacturaRequest
{
    [Required]
    public int IdVenta { get; set; }

    [Required(ErrorMessage = "El número de factura es obligatorio.")]
    public string NumeroFactura { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de comprobante es obligatorio.")]
    public string TipoComprobante { get; set; } = string.Empty;
}