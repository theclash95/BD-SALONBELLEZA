namespace SALONBELLEZA.CORE.Entidades;

public class Factura
{
    public int IdFactura { get; set; }
    public int IdVenta { get; set; }
    public string? NumeroFactura { get; set; }
    public DateTime? FechaEmision { get; set; }
    public string? TipoComprobante { get; set; }
    public string? Estado { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? Descuento { get; set; }
    public decimal? Total { get; set; }
}
