namespace SALONBELLEZA.CORE.Entidades;

public class Turno
{
    public int IdTurno { get; set; }
    public int IdCliente { get; set; }
    public int IdEmpleado { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string? Estado { get; set; }
    public string? Observacion { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
