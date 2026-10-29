using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarProductoRequest
    {
        [Required]
        public int IdCategoriaProducto { get; set; }

        public string? Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal? PrecioCosto { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0.")]
        public decimal PrecioVenta { get; set; }

        public int? StockActual { get; set; }
        public int? StockMinimo { get; set; }
    }
}