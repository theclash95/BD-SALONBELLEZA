using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IMembresiaService
{
    Task<int> CrearMembresiaAsync(Membresia m);
    Task<IEnumerable<Membresia>> ObtenerMembresiasAsync();
    Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm);
    Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente);
    Task RegistrarPagoMembresiaAsync(int idClienteMembresia, decimal monto, string metodoPago, string referencia);
}