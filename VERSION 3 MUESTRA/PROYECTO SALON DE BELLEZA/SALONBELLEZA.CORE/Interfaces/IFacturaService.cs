using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IFacturaService
{
    Task<int> GenerarFacturaAsync(Factura f);
    Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync();
    Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int idFactura);
}