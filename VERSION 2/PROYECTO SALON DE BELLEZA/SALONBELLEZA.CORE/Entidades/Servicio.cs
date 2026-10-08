namespace SALONBELLEZA.CORE.Entidades;

public class Servicio
{
    public int IdServicio { get; set; }
    public int IdCategoriaServicio { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int? DuracionMinuto { get; set; }
    public string? Estado { get; set; }
}
