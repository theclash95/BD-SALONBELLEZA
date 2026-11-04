using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class ClienteService : IClienteService
{
    private readonly IClienteRepositorio _repo;
    public ClienteService(IClienteRepositorio repo) => _repo = repo;

    public Task<int> RegistrarClienteAsync(Persona p, string? categoria)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Apellido))
            throw new ArgumentException("Nombre y apellido son obligatorios.");
        return _repo.RegistrarClienteAsync(p, categoria);
    }

    public Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync() => _repo.ObtenerClientesAsync();
    public Task<ClienteConsulta?> ObtenerClientePorIdAsync(int id) => _repo.ObtenerClientePorIdAsync(id);
    public Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino) => _repo.BuscarClientesAsync(termino);
    public Task ActualizarClienteAsync(int id, string? dni, string nom, string ape, string? tel, string? mail, string? dir) => _repo.ActualizarClienteAsync(id, dni, nom, ape, tel, mail, dir);
    public Task DesactivarClienteAsync(int id) => _repo.DesactivarClienteAsync(id);
}