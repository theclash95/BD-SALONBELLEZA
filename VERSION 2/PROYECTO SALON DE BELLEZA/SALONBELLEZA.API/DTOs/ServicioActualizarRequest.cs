using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class ServicioActualizarRequest
    {
        [Required]
        public int IdServicio { get; set; }

        [Required]
        public int IdCategoriaServicio { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Precio { get; set; }

        public int? DuracionMinuto { get; set; }
    }
}