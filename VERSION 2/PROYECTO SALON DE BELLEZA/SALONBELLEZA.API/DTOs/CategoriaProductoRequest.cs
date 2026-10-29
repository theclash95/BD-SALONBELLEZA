using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class CategoriaProductoRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }
}