
namespace SALONBELLEZA.CORE.Entidades;

public class EmpleadoConsulta
{
    public int IdEmpleado { get; set; }
    public int IdPersona { get; set; }

    public string? Dni { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;

    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }

    public string? TipoEmpleado { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string? Estado { get; set; }
}
