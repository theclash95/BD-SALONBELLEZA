namespace SALONBELLEZA.CORE.Entidades;

public class DetalleTurno
{
    public int IdDetalleTurno { get; set; }
    public int IdTurno { get; set; }
    public int IdServicio { get; set; }
    public decimal? PrecioUnitario { get; set; }
    public decimal? Descuento { get; set; }
    public decimal? Subtotal { get; set; }
}
