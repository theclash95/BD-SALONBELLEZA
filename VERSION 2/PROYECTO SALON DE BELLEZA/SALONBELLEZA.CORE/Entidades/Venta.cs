namespace SALONBELLEZA.CORE.Entidades;

public class Venta
{
    public int IdVenta { get; set; }
    public int IdCliente { get; set; }
    public int? IdTurno { get; set; }
    public DateTime? FechaHora { get; set; }
    public string? TipoVenta { get; set; }
    public string? Estado { get; set; }
    public decimal? Subtotal { get; set; }
    public decimal? Descuento { get; set; }
    public decimal? Total { get; set; }
    public string? Observacion { get; set; }
}
