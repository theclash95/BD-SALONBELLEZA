using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class VentaService : IVentaService
{
    private readonly IVentaRepositorio _repo;
    public VentaService(IVentaRepositorio repo) => _repo = repo;

    public Task<int> CrearVentaDesdeTurnoAsync(int idTurno, decimal descuento, string? observacion)
    {
        if (idTurno <= 0) throw new ArgumentException("El ID del turno no es válido.");
        if (descuento < 0) throw new ArgumentException("El descuento no puede ser negativo.");
        return _repo.CrearVentaDesdeTurnoAsync(idTurno, descuento, observacion);
    }

    public Task<int> RealizarVentaProductoAsync(int idCliente, int idProducto, int cantidad)
    {
        if (idCliente <= 0 || idProducto <= 0 || cantidad <= 0)
            throw new ArgumentException("Datos de venta inválidos.");
        return _repo.RealizarVentaProductoAsync(idCliente, idProducto, cantidad);
    }

    public Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync() => _repo.ObtenerVentasAsync();
    public Task<VentaConsulta?> ObtenerVentaPorIdAsync(int id) => _repo.ObtenerVentaPorIdAsync(id);
}