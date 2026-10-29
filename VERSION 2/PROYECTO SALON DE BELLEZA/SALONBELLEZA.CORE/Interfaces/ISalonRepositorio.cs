using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.CORE.Interfaces;

public interface ISalonRepositorio
{
    // Clientes
    Task<int> RegistrarClienteAsync(Persona persona, string? categoria);
    Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync();
    Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente);
    Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino);
    Task ActualizarClienteAsync(int idCliente, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion);
    Task DesactivarClienteAsync(int idCliente);

    // Empleados

    Task<int> RegistrarEmpleadoAsync(Persona persona, string? tipoEmpleado);
    Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync();
    Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int idEmpleado);
    Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino);
    Task ActualizarEmpleadoAsync(int idEmpleado, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion, string? tipoEmpleado);
    Task DesactivarEmpleadoAsync(int idEmpleado);

    // Proveedores
    Task<int> RegistrarProveedorAsync(Proveedor p);
    Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync();
    Task<Proveedor?> ObtenerProveedorPorIdAsync(int idProveedor); // CON ?
    Task ActualizarProveedorAsync(int idProveedor, string razonSocial, string cuit, string telefono, string email, string direccion);
    Task DesactivarProveedorAsync(int idProveedor);

    // Categorías Producto
    Task<int> RegistrarCategoriaProductoAsync(string nombre, string? descripcion);
    Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync();
    Task ActualizarCategoriaProductoAsync(int idCategoria, string nombre, string? descripcion);
    Task DesactivarCategoriaProductoAsync(int idCategoria);

    // Productos
    Task<int> RegistrarProductoAsync(Producto p);
    Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync();
    Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int idProducto); // CON ?
    Task ActualizarProductoAsync(int idProducto, int idCategoria, string codigo, string nombre, string? descripcion, decimal precioCosto, decimal precioVenta, int stockMinimo);
    Task DesactivarProductoAsync(int idProducto);

    // Stock
    Task<int> ActualizarStockProductoAsync(int idProducto, int cantidad, string tipoMovimiento, string? motivo);
    Task<IEnumerable<MovimientoStockConsulta>> ObtenerMovimientosStockAsync(int idProducto);

    // Compras
    Task<int> RegistrarCompraAsync(int idProveedor, int idProducto, int cantidad, decimal precioCosto, string? observacion);
    Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync();
    Task<CompraConsulta?> ObtenerCompraPorIdAsync(int idCompra); // CON ?

    // Categorías Servicio
    Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion);
    Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync();
    Task ActualizarCategoriaServicioAsync(int idCategoria, string nombre, string? descripcion);
    Task DesactivarCategoriaServicioAsync(int idCategoria);

    // Servicios
    Task<int> RegistrarServicioAsync(Servicio s);
    Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync();
    Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int idServicio); // CON ?
    Task ActualizarServicioAsync(int idServicio, int idCategoria, string nombre, string? descripcion, decimal precio, int? duracion);
    Task DesactivarServicioAsync(int idServicio);

    // Turnos
    Task<int> CrearTurnoAsync(Turno t);
    Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync();
    Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int idTurno); // CON ?
    Task CambiarEstadoTurnoAsync(int idTurno, string estado);
    Task<int> AgregarServicioATurnoAsync(int idTurno, int idServicio, decimal descuento);

    // Ventas
    Task<int> CrearVentaDesdeTurnoAsync(int idTurno, decimal descuento, string? observacion);
    Task<int> RealizarVentaProductoAsync(int idCliente, int idProducto, int cantidad);
    Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync();
    Task<VentaConsulta?> ObtenerVentaPorIdAsync(int idVenta); // CON ?

    // Pagos
    Task<int> RegistrarPagoAsync(Pago p);
    Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta);

    // Facturas
    Task<int> GenerarFacturaAsync(Factura f);
    Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync();
    Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int idFactura);

    // Membresías
    Task<int> CrearMembresiaAsync(Membresia m);
    Task<IEnumerable<Membresia>> ObtenerMembresiasAsync();
    Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm);
    Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente);
    Task RegistrarPagoMembresiaAsync(int idClienteMembresia, decimal monto, string metodoPago, string referencia);

    // Salón
    Task<Salon?> ObtenerSalonAsync(); // CON ?
    Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo);
}