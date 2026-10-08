namespace SALONBELLEZA.API.DTOs;

public class ClienteActualizarRequest
{
    public string? Dni { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Direccion { get; set; }
}