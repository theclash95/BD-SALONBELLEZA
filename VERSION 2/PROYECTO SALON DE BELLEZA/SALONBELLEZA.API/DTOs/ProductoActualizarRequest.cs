using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class ProductoActualizarRequest
    {
        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int IdCategoriaProducto { get; set; }

        public string? Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal? PrecioCosto { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        public decimal PrecioVenta { get; set; }

        public int? StockMinimo { get; set; }
    }
}