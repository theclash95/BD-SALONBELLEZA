using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class ServicioRequest
    {
        [Required] public int IdCategoriaServicio { get; set; }
        [Required] public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        [Required] public decimal Precio { get; set; }
        public int? DuracionMinuto { get; set; }
    }
}