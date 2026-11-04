using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class MembresiaService : IMembresiaService
{
    private readonly IMembresiaRepositorio _repo;
    public MembresiaService(IMembresiaRepositorio repo) => _repo = repo;

    public Task<int> CrearMembresiaAsync(Membresia m)
    {
        if (string.IsNullOrWhiteSpace(m.NombrePlan) || m.Costo < 0)
            throw new ArgumentException("Datos de membresía inválidos.");
        return _repo.CrearMembresiaAsync(m);
    }

    public Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm)
    {
        if (cm.IdCliente <= 0 || cm.IdMembresia <= 0)
            throw new ArgumentException("Cliente y membresía son obligatorios.");
        return _repo.AsignarMembresiaClienteAsync(cm);
    }

    public Task<IEnumerable<Membresia>> ObtenerMembresiasAsync() => _repo.ObtenerMembresiasAsync();
    public Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente) => _repo.ObtenerClienteMembresiasAsync(idCliente);
    public Task RegistrarPagoMembresiaAsync(int idClienteMembresia, decimal monto, string metodoPago, string referencia)
        => _repo.RegistrarPagoMembresiaAsync(idClienteMembresia, monto, metodoPago, referencia);
}