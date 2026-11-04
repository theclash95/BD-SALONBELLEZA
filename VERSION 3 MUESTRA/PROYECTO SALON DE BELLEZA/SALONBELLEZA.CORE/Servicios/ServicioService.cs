using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class ServicioService : IServicioService
{
    private readonly IServicioRepositorio _repo;
    public ServicioService(IServicioRepositorio repo) => _repo = repo;

    public Task<int> RegistrarServicioAsync(Servicio s)
    {
        if (s.IdCategoriaServicio <= 0 || string.IsNullOrWhiteSpace(s.Nombre) || s.Precio < 0)
            throw new ArgumentException("Datos de servicio inválidos.");
        return _repo.RegistrarServicioAsync(s);
    }

    public Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync() => _repo.ObtenerServiciosAsync();
    public Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int id) => _repo.ObtenerServicioPorIdAsync(id);
    public Task ActualizarServicioAsync(int id, int idCat, string nom, string? desc, decimal precio, int? dur) => _repo.ActualizarServicioAsync(id, idCat, nom, desc, precio, dur);
    public Task DesactivarServicioAsync(int id) => _repo.DesactivarServicioAsync(id);
}