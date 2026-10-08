namespace SALONBELLEZA.API.DTOs;

public class VentaRequest
{
    public int IdTurno { get; set; }

    public decimal Descuento { get; set; }

    public string? Observacion { get; set; }
}