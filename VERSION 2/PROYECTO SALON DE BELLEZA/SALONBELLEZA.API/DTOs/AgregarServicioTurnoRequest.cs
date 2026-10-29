using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class AgregarServicioTurnoRequest
    {
        [Required(ErrorMessage = "El ID del servicio es obligatorio.")]
        public int IdServicio { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser negativo.")]
        public decimal Descuento { get; set; } = 0;
    }
}