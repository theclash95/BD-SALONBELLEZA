namespace SALONBELLEZA.API.DTOs;

public class ProductoRequest
{
    public int IdCategoriaProducto { get; set; }
    public string? Codigo { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal? PrecioCosto { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; }
}
