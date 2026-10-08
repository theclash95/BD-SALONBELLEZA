namespace SALONBELLEZA.CORE.Entidades;

public class Producto
{
    public int IdProducto { get; set; }
    public int IdCategoriaProducto { get; set; }
    public string? Codigo { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal? PrecioCosto { get; set; }
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public string? Estado { get; set; }
    public DateTime? FechaAlta { get; set; }
}
