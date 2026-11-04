using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface IProveedorService
{
    Task<int> RegistrarProveedorAsync(Proveedor p);
    Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync();
    Task<Proveedor?> ObtenerProveedorPorIdAsync(int idProveedor);
    Task ActualizarProveedorAsync(int idProveedor, string razonSocial, string cuit, string telefono, string email, string direccion);
    Task DesactivarProveedorAsync(int idProveedor);
}
