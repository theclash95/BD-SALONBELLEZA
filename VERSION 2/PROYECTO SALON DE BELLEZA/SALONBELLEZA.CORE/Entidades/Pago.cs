namespace SALONBELLEZA.CORE.Entidades;

public class Pago
{
    public int IdPago { get; set; }
    public int IdVenta { get; set; }
    public DateTime? FechaPago { get; set; }
    public decimal? Monto { get; set; }
    public string? MetodoPago { get; set; }
    public string? Estado { get; set; }
    public string? Referencia { get; set; }
}
