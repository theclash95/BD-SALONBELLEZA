using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IPagoService
{
    Task<int> RegistrarPagoAsync(Pago p);
    Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta);
}