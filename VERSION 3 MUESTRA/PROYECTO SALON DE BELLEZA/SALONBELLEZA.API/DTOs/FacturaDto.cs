using System;
namespace SALONBELLEZA.API.DTOs
{
    public class FacturaDto
    {
        public int IdFactura { get; set; }
        public int IdVenta { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string? TipoComprobante { get; set; }
        public decimal? Total { get; set; }
    }
}