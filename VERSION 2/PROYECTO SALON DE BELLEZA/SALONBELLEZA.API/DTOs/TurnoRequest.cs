namespace SALONBELLEZA.API.DTOs;

public class TurnoRequest
{
    public int IdCliente { get; set; }
    public int IdEmpleado { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string? Observacion { get; set; }
}
