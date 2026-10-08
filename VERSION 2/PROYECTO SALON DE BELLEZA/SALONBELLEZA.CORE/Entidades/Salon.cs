namespace SALONBELLEZA.CORE.Entidades;

public class Salon
{
    public int IdSalon { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Logo { get; set; }
    public bool Activo { get; set; }
    public DateTime? FechaAlta { get; set; }
}
