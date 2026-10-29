using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class CrearVentaDesdeTurnoRequest
    {
        [Required] public int IdTurno { get; set; }
        public decimal Descuento { get; set; } = 0;
        public string? Observacion { get; set; }
    }
}