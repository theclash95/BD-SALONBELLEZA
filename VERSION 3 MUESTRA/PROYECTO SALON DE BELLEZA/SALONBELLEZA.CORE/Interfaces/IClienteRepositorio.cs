using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IClienteRepositorio
{
    Task<int> RegistrarClienteAsync(Persona persona, string? categoria);
    Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync();
    Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente);
    Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino);
    Task ActualizarClienteAsync(int idCliente, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion);
    Task DesactivarClienteAsync(int idCliente);
}