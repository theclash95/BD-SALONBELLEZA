namespace SALONBELLEZA.API.DTOs;

public class RegistrarClienteRequest
{
    public string? Dni { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? Categoria { get; set; }
}
