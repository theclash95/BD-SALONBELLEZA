using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class StockRequest
    {
        [Required] public int IdProducto { get; set; }
        [Required] public int Cantidad { get; set; }
        [Required] public string TipoMovimiento { get; set; } = string.Empty; // "INGRESO" o "EGRESO"
        public string? Motivo { get; set; }
    }
}