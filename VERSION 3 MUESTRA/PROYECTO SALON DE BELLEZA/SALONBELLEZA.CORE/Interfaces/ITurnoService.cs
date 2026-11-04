using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ITurnoService
{
    Task<int> CrearTurnoAsync(Turno t);
    Task<int> AgregarServicioATurnoAsync(int idTurno, int idServicio, decimal descuento);
    Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync();
    Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int idTurno);
    Task CambiarEstadoTurnoAsync(int idTurno, string estado);
}