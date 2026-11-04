using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IPagoRepositorio
{
    Task<int> RegistrarPagoAsync(Pago p);
    Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta);
}