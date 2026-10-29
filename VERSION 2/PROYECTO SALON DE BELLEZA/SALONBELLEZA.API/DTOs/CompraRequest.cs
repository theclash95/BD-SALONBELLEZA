using System.ComponentModel.DataAnnotations;

namespace SALONBELLEZA.API.DTOs;

public class CompraRequest
{
    [Required]
    public int IdProveedor { get; set; }

    [Required]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; set; }

    [Required]
    public decimal PrecioCosto { get; set; }

    public string? Observacion { get; set; }
}