namespace SALONBELLEZA.API.DTOs;

public class AsignarMembresiaRequest
{
    public int IdCliente { get; set; }
    public int IdMembresia { get; set; }
    public string? Observacion { get; set; }
}
