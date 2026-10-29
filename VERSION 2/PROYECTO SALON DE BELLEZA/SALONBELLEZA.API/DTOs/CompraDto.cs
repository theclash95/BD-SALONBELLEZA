using System;
namespace SALONBELLEZA.API.DTOs
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public DateTime? FechaHora { get; set; }
        public decimal? Total { get; set; }
    }
}