using System.ComponentModel.DataAnnotations;

public class StockRequest
{
    [Required(ErrorMessage = "El ID del producto es obligatorio.")]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El tipo de movimiento es obligatorio (INGRESO o EGRESO).")]
    public string TipoMovimiento { get; set; } = string.Empty;

    public string? Motivo { get; set; }
}