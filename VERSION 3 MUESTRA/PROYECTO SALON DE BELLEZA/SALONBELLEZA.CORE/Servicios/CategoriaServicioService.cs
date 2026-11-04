using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class CategoriaServicioService : ICategoriaServicioService
{
    private readonly ICategoriaServicioRepositorio _repo;
    public CategoriaServicioService(ICategoriaServicioRepositorio repo) => _repo = repo;

    public Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
        return _repo.RegistrarCategoriaServicioAsync(nombre, descripcion);
    }

    public Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync() => _repo.ObtenerCategoriasServicioAsync();
    public Task ActualizarCategoriaServicioAsync(int id, string nombre, string? desc) => _repo.ActualizarCategoriaServicioAsync(id, nombre, desc);
    public Task DesactivarCategoriaServicioAsync(int id) => _repo.DesactivarCategoriaServicioAsync(id);
}
