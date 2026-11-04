using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ITurnoRepositorio
{
    Task<int> CrearTurnoAsync(Turno t);
    Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync();
    Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int idTurno);
    Task CambiarEstadoTurnoAsync(int idTurno, string estado);
    Task<int> AgregarServicioATurnoAsync(int idTurno, int idServicio, decimal descuento);
}