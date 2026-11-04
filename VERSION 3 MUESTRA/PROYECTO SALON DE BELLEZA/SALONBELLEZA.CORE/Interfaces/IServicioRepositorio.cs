using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IServicioRepositorio
{
    Task<int> RegistrarServicioAsync(Servicio s);
    Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync();
    Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int idServicio);
    Task ActualizarServicioAsync(int idServicio, int idCategoria, string nombre, string? descripcion, decimal precio, int? duracion);
    Task DesactivarServicioAsync(int idServicio);
}