namespace SALONBELLEZA.CORE.Entidades;

public class ClienteMembresia
{
    public int IdClienteMembresia { get; set; }
    public int IdCliente { get; set; }
    public int IdMembresia { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Estado { get; set; }
    public string? Observacion { get; set; }
}
