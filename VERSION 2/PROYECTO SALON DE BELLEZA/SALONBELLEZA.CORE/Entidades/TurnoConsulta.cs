
namespace SALONBELLEZA.CORE.Entidades;

public class TurnoConsulta
{
    public int IdTurno { get; set; }

    public int IdCliente { get; set; }
    public string? NombreCliente { get; set; }

    public int IdEmpleado { get; set; }
    public string? NombreEmpleado { get; set; }

    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }

    public string? Estado { get; set; }
    public string? Observacion { get; set; }
}
