using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class FacturaRequest
    {
        [Required] public int IdVenta { get; set; }
        [Required] public string NumeroFactura { get; set; } = string.Empty;
        [Required] public string TipoComprobante { get; set; } = string.Empty;
    }
}