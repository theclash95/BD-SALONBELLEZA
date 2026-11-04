using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class PagoService : IPagoService
{
    private readonly IPagoRepositorio _repo;
    public PagoService(IPagoRepositorio repo) => _repo = repo;

    public Task<int> RegistrarPagoAsync(Pago p)
    {
        if (p.IdVenta <= 0 || p.Monto is null || p.Monto < 0)
            throw new ArgumentException("Datos de pago inválidos.");
        return _repo.RegistrarPagoAsync(p);
    }

    public Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta) => _repo.ObtenerPagosPorVentaAsync(idVenta);
}