using System;
namespace SALONBELLEZA.API.DTOs
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime? FechaHora { get; set; }
        public string? TipoVenta { get; set; }
        public string? Estado { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
    }
}