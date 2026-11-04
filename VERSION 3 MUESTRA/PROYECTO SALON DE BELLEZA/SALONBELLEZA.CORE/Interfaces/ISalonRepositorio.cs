using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ISalonRepositorio
{
    Task<Salon?> ObtenerSalonAsync();
    Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo);
}