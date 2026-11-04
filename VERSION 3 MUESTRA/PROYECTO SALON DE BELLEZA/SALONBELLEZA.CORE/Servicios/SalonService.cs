using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.CORE.Servicios;

public class SalonService : ISalonService
{
    private readonly ISalonRepositorio _repo;
    public SalonService(ISalonRepositorio repo) => _repo = repo;

    public Task<Salon?> ObtenerSalonAsync() => _repo.ObtenerSalonAsync();

    public Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del salón es obligatorio.");
        return _repo.ActualizarSalonAsync(idSalon, nombre, direccion, telefono, email, logo);
    }
}