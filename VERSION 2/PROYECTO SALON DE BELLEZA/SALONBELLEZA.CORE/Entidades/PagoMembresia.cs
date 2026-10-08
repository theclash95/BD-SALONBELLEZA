namespace SALONBELLEZA.CORE.Entidades;

public class PagoMembresia
{
    public int IdPagoMembresia { get; set; }
    public int IdClienteMembresia { get; set; }
    public DateTime? FechaPago { get; set; }
    public decimal? MontoPago { get; set; }
    public string? MetodoPago { get; set; }
    public string? Estado { get; set; }
    public string? Referencia { get; set; }
}
