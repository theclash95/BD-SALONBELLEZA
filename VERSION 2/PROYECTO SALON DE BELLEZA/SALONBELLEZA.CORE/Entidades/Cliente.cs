namespace SALONBELLEZA.CORE.Entidades;

public class Cliente
{
    public int IdCliente { get; set; }
    public int IdPersona { get; set; }
    public string? CategoriaCliente { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public string? Estado { get; set; }
}
