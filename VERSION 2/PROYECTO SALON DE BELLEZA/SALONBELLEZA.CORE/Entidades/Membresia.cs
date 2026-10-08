namespace SALONBELLEZA.CORE.Entidades;

public class Membresia
{
    public int IdMembresia { get; set; }
    public string NombrePlan { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
    public int? DuracionDias { get; set; }
    public decimal? PorcentajeDescuento { get; set; }
    public string? Estado { get; set; }
}
