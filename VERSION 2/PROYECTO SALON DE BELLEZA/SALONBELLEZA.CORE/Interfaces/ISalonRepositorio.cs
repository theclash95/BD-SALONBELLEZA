using SALONBELLEZA.CORE.Entidades;
namespace SALONBELLEZA.CORE.Interfaces;
public interface ISalonRepositorio
{
    Task<int> RegistrarClienteAsync(Persona persona, string? categoria);
    Task<int> RegistrarEmpleadoAsync(Persona persona, string? tipoEmpleado);
    Task<int> RegistrarCategoriaProductoAsync(string nombre, string? descripcion);
    Task<int> RegistrarProductoAsync(Producto p);
    Task<int> ActualizarStockProductoAsync(int idProducto,int cantidad,string tipoMovimiento,string? motivo);
    Task<int> RegistrarProveedorAsync(Proveedor p);
    Task<int> RegistrarCompraAsync(int idProveedor,int idProducto,int cantidad,decimal precioCosto,string? observacion);
    Task<int> RegistrarServicioAsync(Servicio s);
    Task<int> CrearTurnoAsync(Turno t);
    Task<int> AgregarServicioATurnoAsync(int idTurno,int idServicio,decimal descuento);
    Task<int> CrearVentaDesdeTurnoAsync(
    int idTurno,
    decimal descuento,
    string? observacion);

    Task<int> RealizarVentaProductoAsync(int idCliente,int idProducto,int cantidad);
    Task<int> RegistrarPagoAsync(Pago p);
    Task<int> GenerarFacturaAsync(Factura f);
    Task<int> CrearMembresiaAsync(Membresia m);
    Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm);
    Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion);

}