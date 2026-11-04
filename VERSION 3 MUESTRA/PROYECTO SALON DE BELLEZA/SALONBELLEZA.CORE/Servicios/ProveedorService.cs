using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class ProveedorService : IProveedorService
{
    private readonly IProveedorRepositorio _repo;
    public ProveedorService(IProveedorRepositorio repo) => _repo = repo;

    public Task<int> RegistrarProveedorAsync(Proveedor p)
    {
        if (string.IsNullOrWhiteSpace(p.RazonSocial))
            throw new ArgumentException("La razón social es obligatoria.");
        return _repo.RegistrarProveedorAsync(p);
    }

    public Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync() => _repo.ObtenerProveedoresAsync();
    public Task<Proveedor?> ObtenerProveedorPorIdAsync(int id) => _repo.ObtenerProveedorPorIdAsync(id);
    public Task ActualizarProveedorAsync(int id, string razon, string cuit, string tel, string mail, string dir) => _repo.ActualizarProveedorAsync(id, razon, cuit, tel, mail, dir);
    public Task DesactivarProveedorAsync(int id) => _repo.DesactivarProveedorAsync(id);
}