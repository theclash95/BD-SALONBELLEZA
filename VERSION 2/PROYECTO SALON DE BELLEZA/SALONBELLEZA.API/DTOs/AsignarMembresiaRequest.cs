using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs
{
    public class AsignarMembresiaRequest
    {
        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La membresía es obligatoria.")]
        public int IdMembresia { get; set; }

        public string? Observacion { get; set; }
    }
}