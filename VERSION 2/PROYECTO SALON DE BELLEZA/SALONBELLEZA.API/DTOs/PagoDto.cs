using System;
namespace SALONBELLEZA.API.DTOs
{
    public class PagoDto
    {
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? Monto { get; set; }
        public string? MetodoPago { get; set; }
        public string? Referencia { get; set; }
    }
}