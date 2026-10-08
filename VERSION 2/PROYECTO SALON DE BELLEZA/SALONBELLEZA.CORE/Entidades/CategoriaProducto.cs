namespace SALONBELLEZA.CORE.Entidades;

public class CategoriaProducto
{
    public int IdCategoriaProducto { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}
