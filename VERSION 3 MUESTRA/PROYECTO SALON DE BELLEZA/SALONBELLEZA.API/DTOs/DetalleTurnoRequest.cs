using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class DetalleTurnoRequest
    {
        [Required] public int IdTurno { get; set; }
        [Required] public int IdServicio { get; set; }
        public decimal Descuento { get; set; }
    }
}