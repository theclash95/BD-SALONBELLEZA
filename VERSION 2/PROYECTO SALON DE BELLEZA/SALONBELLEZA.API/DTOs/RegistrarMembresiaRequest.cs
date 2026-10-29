using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarMembresiaRequest
    {
        [Required] public string NombrePlan { get; set; } = string.Empty;
        [Required] public decimal Costo { get; set; }
        [Required] public int DuracionDias { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
    }
}