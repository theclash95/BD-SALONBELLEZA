using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class ProveedorRequest
{
    [Required] public string RazonSocial { get; set; } = string.Empty;
    public string? Cuit { get; set; }
    public string? Telefono { get; set; }
    [EmailAddress] public string? Email { get; set; }
    public string? Direccion { get; set; }
}