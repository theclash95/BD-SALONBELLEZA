using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IProductoRepositorio
{
    Task<int> RegistrarProductoAsync(Producto p);
    Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync();
    Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int idProducto);
    Task ActualizarProductoAsync(int idProducto, int idCategoria, string codigo, string nombre, string? descripcion, decimal precioCosto, decimal precioVenta, int stockMinimo);
    Task DesactivarProductoAsync(int idProducto);

    // Métodos de Stock integrados acá
    Task<int> ActualizarStockProductoAsync(int idProducto, int cantidad, string tipoMovimiento, string? motivo);
    Task<IEnumerable<MovimientoStockConsulta>> ObtenerMovimientosStockAsync(int idProducto);
}