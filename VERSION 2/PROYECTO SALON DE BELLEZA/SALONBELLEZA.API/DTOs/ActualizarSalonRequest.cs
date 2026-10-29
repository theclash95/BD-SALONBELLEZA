using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class ActualizarSalonRequest
{
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
    public string? Email { get; set; }

    public string? Logo { get; set; }
}