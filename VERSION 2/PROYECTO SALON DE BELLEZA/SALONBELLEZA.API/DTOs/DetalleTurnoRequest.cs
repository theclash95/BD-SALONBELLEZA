namespace SALONBELLEZA.API.DTOs;

public class DetalleTurnoRequest
{
    public int IdTurno { get; set; }
    public int IdServicio { get; set; }
    public decimal Descuento { get; set; }
}
