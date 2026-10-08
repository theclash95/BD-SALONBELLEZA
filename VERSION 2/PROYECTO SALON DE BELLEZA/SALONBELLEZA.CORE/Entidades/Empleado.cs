namespace SALONBELLEZA.CORE.Entidades;

public class Empleado
{
    public int IdEmpleado { get; set; }
    public int IdPersona { get; set; }
    public string? TipoEmpleado { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string? Estado { get; set; }
}
