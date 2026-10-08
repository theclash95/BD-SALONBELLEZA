namespace SALONBELLEZA.API.DTOs;

public class MembresiaRequest
{
    public string NombrePlan { get; set; }
    public string? Descripcion { get; set; }
    public decimal Costo { get; set; }
    public int DuracionDias { get; set; }
    public decimal PorcentajeDescuento { get; set; }
}
