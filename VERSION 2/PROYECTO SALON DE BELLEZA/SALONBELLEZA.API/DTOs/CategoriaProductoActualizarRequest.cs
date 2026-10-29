using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class CategoriaProductoActualizarRequest
    {
        [Required]
        public int IdCategoriaProducto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
    }
}