using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarPagoMembresiaRequest
    {
        [Required] public int IdClienteMembresia { get; set; }
        [Required] public decimal Monto { get; set; }
        [Required] public string MetodoPago { get; set; } = string.Empty;
        public string? Referencia { get; set; }
    }
}