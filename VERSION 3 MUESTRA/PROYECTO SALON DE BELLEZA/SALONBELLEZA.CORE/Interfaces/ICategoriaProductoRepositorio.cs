using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ICategoriaProductoRepositorio
{
    Task<int> RegistrarCategoriaProductoAsync(string nombre, string? descripcion);
    Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync();
    Task ActualizarCategoriaProductoAsync(int idCategoria, string nombre, string? descripcion);
    Task DesactivarCategoriaProductoAsync(int idCategoria);
}