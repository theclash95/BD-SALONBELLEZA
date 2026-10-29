using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarServicioRequest
    {
        [Required]
        public int IdCategoriaServicio { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        public int? DuracionMinuto { get; set; } // En minutos (ej: 30, 60)
    }
}