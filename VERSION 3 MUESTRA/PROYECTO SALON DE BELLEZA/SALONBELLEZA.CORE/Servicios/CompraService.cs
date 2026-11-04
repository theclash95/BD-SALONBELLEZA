using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class CompraService : ICompraService
{
    private readonly ICompraRepositorio _repo;
    public CompraService(ICompraRepositorio repo) => _repo = repo;

    public Task<int> RegistrarCompraAsync(int idProv, int idProd, int cant, decimal costo, string? obs)
    {
        if (idProv <= 0 || idProd <= 0 || cant <= 0 || costo < 0)
            throw new ArgumentException("Datos de compra inválidos.");
        return _repo.RegistrarCompraAsync(idProv, idProd, cant, costo, obs);
    }

    public Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync() => _repo.ObtenerComprasAsync();
    public Task<CompraConsulta?> ObtenerCompraPorIdAsync(int id) => _repo.ObtenerCompraPorIdAsync(id);
}