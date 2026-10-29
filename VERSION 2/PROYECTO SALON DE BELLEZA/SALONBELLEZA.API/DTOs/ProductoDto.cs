namespace SALONBELLEZA.API.DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public string NombreCategoria { get; set; } = string.Empty; // Viene del JOIN
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal? PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int? StockActual { get; set; }
        public int? StockMinimo { get; set; }
        public string? Estado { get; set; }
    }
}