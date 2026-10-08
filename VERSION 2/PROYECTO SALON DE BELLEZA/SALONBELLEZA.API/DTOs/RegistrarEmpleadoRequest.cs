namespace SALONBELLEZA.API.DTOs;

public class RegistrarEmpleadoRequest
{
    public string? Dni { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? TipoEmpleado { get; set; }
}
