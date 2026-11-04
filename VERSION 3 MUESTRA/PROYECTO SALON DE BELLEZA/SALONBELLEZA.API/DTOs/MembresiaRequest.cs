using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class MembresiaRequest
    {
        [Required] public string NombrePlan { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        [Required] public decimal Costo { get; set; }
        [Required] public int DuracionDias { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
    }
}