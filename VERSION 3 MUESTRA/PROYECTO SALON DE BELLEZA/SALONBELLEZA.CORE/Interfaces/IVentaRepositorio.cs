using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IVentaRepositorio
{
    Task<int> CrearVentaDesdeTurnoAsync(int idTurno, decimal descuento, string? observacion);
    Task<int> RealizarVentaProductoAsync(int idCliente, int idProducto, int cantidad);
    Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync();
    Task<VentaConsulta?> ObtenerVentaPorIdAsync(int idVenta);
}