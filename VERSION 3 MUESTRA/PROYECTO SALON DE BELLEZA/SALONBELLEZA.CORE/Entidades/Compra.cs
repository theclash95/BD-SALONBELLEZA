namespace SALONBELLEZA.CORE.Entidades;

public class Compra
{
    public int IdCompra { get; set; }
    public int IdProveedor { get; set; }
    public DateTime? FechaHora { get; set; }
    public string? Estado { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? Total { get; set; }
    public string? Observacion { get; set; }
}
