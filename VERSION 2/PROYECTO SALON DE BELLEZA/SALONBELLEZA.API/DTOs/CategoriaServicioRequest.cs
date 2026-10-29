using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class CategoriaServicioRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }
}