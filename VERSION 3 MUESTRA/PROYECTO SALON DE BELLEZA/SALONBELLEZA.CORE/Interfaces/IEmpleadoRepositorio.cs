using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IEmpleadoRepositorio
{
    Task<int> RegistrarEmpleadoAsync(Persona persona, string? tipoEmpleado);
    Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync();
    Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int idEmpleado);
    Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino);
    Task ActualizarEmpleadoAsync(int idEmpleado, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion, string? tipoEmpleado);
    Task DesactivarEmpleadoAsync(int idEmpleado);
}