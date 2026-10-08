namespace SALONBELLEZA.API.DTOs;

public class FacturaRequest
{
    public int IdVenta { get; set; }
    public string NumeroFactura { get; set; }
    public string TipoComprobante { get; set; }
}
