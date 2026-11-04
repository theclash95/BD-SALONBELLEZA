using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class ProductoService : IProductoService
{
    private readonly IProductoRepositorio _repo;
    public ProductoService(IProductoRepositorio repo) => _repo = repo;

    public Task<int> RegistrarProductoAsync(Producto p)
    {
        if (p.IdCategoriaProducto <= 0 || string.IsNullOrWhiteSpace(p.Nombre) || p.PrecioVenta < 0)
            throw new ArgumentException("Datos de producto inválidos.");
        return _repo.RegistrarProductoAsync(p);
    }

    public Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync() => _repo.ObtenerProductosAsync();
    public Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int id) => _repo.ObtenerProductoPorIdAsync(id);
    public Task ActualizarProductoAsync(int id, int idCat, string cod, string nom, string? desc, decimal pCosto, decimal pVenta, int stkMin) => _repo.ActualizarProductoAsync(id, idCat, cod, nom, desc, pCosto, pVenta, stkMin);
    public Task DesactivarProductoAsync(int id) => _repo.DesactivarProductoAsync(id);
    public Task<int> ActualizarStockProductoAsync(int id, int cant, string tipo, string? motivo) => _repo.ActualizarStockProductoAsync(id, cant, tipo, motivo);
    public Task<IEnumerable<MovimientoStockConsulta>> ObtenerMovimientosStockAsync(int id) => _repo.ObtenerMovimientosStockAsync(id);
}