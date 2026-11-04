using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ICategoriaServicioRepositorio
{
    Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion);
    Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync();
    Task ActualizarCategoriaServicioAsync(int idCategoria, string nombre, string? descripcion);
    Task DesactivarCategoriaServicioAsync(int idCategoria);
}