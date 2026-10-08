namespace SALONBELLEZA.API.DTOs;

public class PagoRequest
{
    public int IdVenta { get; set; }
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; }
    public string? Referencia { get; set; }
}
