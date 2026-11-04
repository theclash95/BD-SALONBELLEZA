using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class CategoriaProductoService : ICategoriaProductoService
{
    private readonly ICategoriaProductoRepositorio _repo;
#pragma warning disable IDE0290 // Usar constructor principal
    public CategoriaProductoService(ICategoriaProductoRepositorio repo) => _repo = repo;
#pragma warning restore IDE0290 // Usar constructor principal

    public Task<int> RegistrarCategoriaProductoAsync(string nombre, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
        return _repo.RegistrarCategoriaProductoAsync(nombre, descripcion);
    }

    public Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync() => _repo.ObtenerCategoriasProductoAsync();
    public Task ActualizarCategoriaProductoAsync(int id, string nombre, string? desc) => _repo.ActualizarCategoriaProductoAsync(id, nombre, desc);
    public Task DesactivarCategoriaProductoAsync(int id) => _repo.DesactivarCategoriaProductoAsync(id);
}