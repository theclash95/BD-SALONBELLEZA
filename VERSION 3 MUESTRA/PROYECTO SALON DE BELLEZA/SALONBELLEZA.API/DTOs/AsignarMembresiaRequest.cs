using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class AsignarMembresiaRequest
    {
        [Required] public int IdCliente { get; set; }
        [Required] public int IdMembresia { get; set; }
        public string? Observacion { get; set; }
    }
}