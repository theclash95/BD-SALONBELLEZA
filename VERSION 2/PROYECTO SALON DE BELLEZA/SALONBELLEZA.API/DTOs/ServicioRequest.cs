namespace SALONBELLEZA.API.DTOs;

public class ServicioRequest
{
    public int IdCategoria { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int? Duracion { get; set; }
}
