using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class ServicioRequest
{
    [Required] public int IdCategoria { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Precio { get; set; }
    public int? Duracion { get; set; }
}
