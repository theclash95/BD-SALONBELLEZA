namespace SALONBELLEZA.CORE.Entidades;

public class MovimientoStock
{
    public int IdMovimiento { get; set; }
    public int IdProducto { get; set; }
    public int? IdDetalleVenta { get; set; }
    public string? TipoMovimiento { get; set; }
    public int? Cantidad { get; set; }
    public DateTime? FechaHora { get; set; }
    public string? Motivo { get; set; }
    public int? StockAnterior { get; set; }
    public int? StockPosterior { get; set; }
}
