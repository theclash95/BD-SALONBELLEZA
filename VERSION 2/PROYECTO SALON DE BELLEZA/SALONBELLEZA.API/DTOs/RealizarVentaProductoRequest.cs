using System.ComponentModel.DataAnnotations;
namespace SALONBELLEZA.API.DTOs
{
    public class RealizarVentaProductoRequest
    {
        [Required] public int IdCliente { get; set; }
        [Required] public int IdProducto { get; set; }
        [Required] public int Cantidad { get; set; }
    }
}