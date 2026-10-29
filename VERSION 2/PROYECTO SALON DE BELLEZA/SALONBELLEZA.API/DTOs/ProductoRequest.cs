using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class ProductoRequest
{
    [Required] public int IdCategoriaProducto { get; set; }
    public string? Codigo { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal? PrecioCosto { get; set; }
    [Required] public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; }
}
