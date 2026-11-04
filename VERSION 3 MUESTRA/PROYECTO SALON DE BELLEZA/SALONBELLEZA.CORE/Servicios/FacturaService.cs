using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.CORE.Servicios;

public class FacturaService : IFacturaService
{
    private readonly IFacturaRepositorio _repo;
    public FacturaService(IFacturaRepositorio repo) => _repo = repo;

    public Task<int> GenerarFacturaAsync(Factura f)
    {
        if (f.IdVenta <= 0 || string.IsNullOrWhiteSpace(f.NumeroFactura) || string.IsNullOrWhiteSpace(f.TipoComprobante))
            throw new ArgumentException("Datos de factura inválidos.");
        return _repo.GenerarFacturaAsync(f);
    }

    public Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync() => _repo.ObtenerFacturasAsync();
    public Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int id) => _repo.ObtenerFacturaPorIdAsync(id);
}