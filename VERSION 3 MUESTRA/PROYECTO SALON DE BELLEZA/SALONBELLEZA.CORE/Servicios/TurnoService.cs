using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.CORE.Servicios;
namespace SALONBELLEZA.CORE.Servicios;

public class TurnoService : ITurnoService
{
    private readonly ITurnoRepositorio _repo;
    public TurnoService(ITurnoRepositorio repo) => _repo = repo;

    public Task<int> CrearTurnoAsync(Turno t)
    {
        if (t.IdCliente <= 0 || t.IdEmpleado <= 0)
            throw new ArgumentException("Cliente y empleado son obligatorios.");
        return _repo.CrearTurnoAsync(t);
    }

    public Task<int> AgregarServicioATurnoAsync(int idTurno, int idServicio, decimal descuento)
    {
        if (idTurno <= 0 || idServicio <= 0 || descuento < 0)
            throw new ArgumentException("Datos de detalle de turno inválidos.");
        return _repo.AgregarServicioATurnoAsync(idTurno, idServicio, descuento);
    }

    public Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync() => _repo.ObtenerTurnosAsync();
    public Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int id) => _repo.ObtenerTurnoPorIdAsync(id);
    public Task CambiarEstadoTurnoAsync(int id, string estado) => _repo.CambiarEstadoTurnoAsync(id, estado);
}