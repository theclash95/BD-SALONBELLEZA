namespace SALONBELLEZA.API.DTOs;

public class StockRequest
{
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public string TipoMovimiento { get; set; }
    public string? Motivo { get; set; }
}
