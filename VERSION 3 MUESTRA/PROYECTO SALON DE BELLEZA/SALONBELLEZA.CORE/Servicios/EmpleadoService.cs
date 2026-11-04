using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class EmpleadoService : IEmpleadoService
{
    private readonly IEmpleadoRepositorio _repo;
    public EmpleadoService(IEmpleadoRepositorio repo) => _repo = repo;

    public Task<int> RegistrarEmpleadoAsync(Persona p, string? tipo)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Apellido))
            throw new ArgumentException("Nombre y apellido son obligatorios.");
        return _repo.RegistrarEmpleadoAsync(p, tipo);
    }

    public Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync() => _repo.ObtenerEmpleadosAsync();
    public Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int id) => _repo.ObtenerEmpleadoPorIdAsync(id);
    public Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino) => _repo.BuscarEmpleadosAsync(termino);
    public Task ActualizarEmpleadoAsync(int id, string? dni, string nom, string ape, string? tel, string? mail, string? dir, string? tipo) => _repo.ActualizarEmpleadoAsync(id, dni, nom, ape, tel, mail, dir, tipo);
    public Task DesactivarEmpleadoAsync(int id) => _repo.DesactivarEmpleadoAsync(id);
}