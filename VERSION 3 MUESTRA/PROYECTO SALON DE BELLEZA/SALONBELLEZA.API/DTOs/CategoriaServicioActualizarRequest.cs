using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class CategoriaServicioActualizarRequest
    {
        [Required] public int IdCategoriaServicio { get; set; }
        [Required] public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}