using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ICompraRepositorio
{
    Task<int> RegistrarCompraAsync(int idProveedor, int idProducto, int cantidad, decimal precioCosto, string? observacion);
    Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync();
    Task<CompraConsulta?> ObtenerCompraPorIdAsync(int idCompra);
}