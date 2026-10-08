namespace SALONBELLEZA.API.DTOs;

public class CompraRequest
{
    public int IdProveedor { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioCosto { get; set; }
    public string? Observacion { get; set; }
}
