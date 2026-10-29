using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class RegistrarCompraRequest
    {
        [Required] public int IdProveedor { get; set; }
        [Required] public int IdProducto { get; set; }
        [Required] public int Cantidad { get; set; }
        [Required] public decimal PrecioCosto { get; set; }
        public string? Observacion { get; set; }
    }
}