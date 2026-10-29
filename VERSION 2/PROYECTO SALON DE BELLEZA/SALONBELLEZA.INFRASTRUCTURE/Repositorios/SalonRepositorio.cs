using MySqlConnector;
using Dapper;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SALONBELLEZA.INFRASTRUCTURE.Repositorios;

public class SalonRepositorio : ISalonRepositorio
{
    private readonly IMyConexion _conn;
    public SalonRepositorio(IMyConexion conn) => _conn = conn;

    private async Task<int> EjecutarIdAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
        using var q = new MySqlCommand("SELECT LAST_INSERT_ID();", c);
        var v = await q.ExecuteScalarAsync();
        return Convert.ToInt32(v);
    }

    private async Task EjecutarAsync(string proc, params (string, string?, object?)[] ps)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(proc, c) { CommandType = CommandType.StoredProcedure };
        foreach (var p in ps) cmd.Parameters.AddWithValue(p.Item1, p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    // CLIENTES
    public Task<int> RegistrarClienteAsync(Persona p, string? cat) => EjecutarIdAsync("RegistrarCliente",
        ("p_dni", null, p.Dni), ("p_nombre", null, p.Nombre), ("p_apellido", null, p.Apellido),
        ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion), ("p_categoria", null, cat));

    public async Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteConsulta>("ObtenerClientes", commandType: CommandType.StoredProcedure);
    }

    public async Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ClienteConsulta>("ObtenerClientePorId", new { p_id_cliente = idCliente }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteConsulta>("BuscarClientes", new { p_termino = termino }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarClienteAsync(int idCliente, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion) => EjecutarAsync("ActualizarCliente",
        ("p_id_cliente", null, idCliente), ("p_dni", null, dni), ("p_nombre", null, nombre),
        ("p_apellido", null, apellido), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion));

    public Task DesactivarClienteAsync(int idCliente) => EjecutarAsync("DesactivarCliente", ("p_id_cliente", null, idCliente));

    // EMPLEADOS
    public Task<int> RegistrarEmpleadoAsync(Persona p, string? tipo) => EjecutarIdAsync("RegistrarEmpleado",
        ("p_dni", null, p.Dni), ("p_nombre", null, p.Nombre), ("p_apellido", null, p.Apellido),
        ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion), ("p_tipo_empleado", null, tipo));

    public async Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<EmpleadoConsulta>("ObtenerEmpleados", commandType: CommandType.StoredProcedure);
    }

    public async Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int idEmpleado)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<EmpleadoConsulta>("ObtenerEmpleadoPorId", new { p_id_empleado = idEmpleado }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<EmpleadoConsulta>("BuscarEmpleados", new { p_termino = termino }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarEmpleadoAsync(int idEmpleado, string? dni, string nombre, string apellido, string? telefono, string? email, string? direccion, string? tipoEmpleado) => EjecutarAsync("ActualizarEmpleado",
        ("p_id_empleado", null, idEmpleado), ("p_dni", null, dni), ("p_nombre", null, nombre),
        ("p_apellido", null, apellido), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion), ("p_tipo_empleado", null, tipoEmpleado));

    public Task DesactivarEmpleadoAsync(int idEmpleado) => EjecutarAsync("DesactivarEmpleado", ("p_id_empleado", null, idEmpleado));

    // PROVEEDORES
    public Task<int> RegistrarProveedorAsync(Proveedor p) => EjecutarIdAsync("RegistrarProveedor",
        ("p_razon_social", null, p.RazonSocial), ("p_cuit", null, p.Cuit), ("p_telefono", null, p.Telefono), ("p_email", null, p.Email), ("p_direccion", null, p.Direccion));

    public async Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<Proveedor>("ObtenerProveedores", commandType: CommandType.StoredProcedure);
    }

    public async Task<Proveedor?> ObtenerProveedorPorIdAsync(int idProveedor)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<Proveedor>("ObtenerProveedorPorId", new { p_id_proveedor = idProveedor }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarProveedorAsync(int idProveedor, string razonSocial, string cuit, string telefono, string email, string direccion) => EjecutarAsync("ActualizarProveedor",
        ("p_id_proveedor", null, idProveedor), ("p_razon_social", null, razonSocial), ("p_cuit", null, cuit), ("p_telefono", null, telefono), ("p_email", null, email), ("p_direccion", null, direccion));

    public Task DesactivarProveedorAsync(int idProveedor) => EjecutarAsync("DesactivarProveedor", ("p_id_proveedor", null, idProveedor));

    // CATEGORIAS PRODUCTO
    public Task<int> RegistrarCategoriaProductoAsync(string n, string? d) => EjecutarIdAsync("RegistrarCategoriaProducto", ("p_nombre", null, n), ("p_descripcion", null, d));

    public async Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CategoriaProducto>("ObtenerCategoriasProducto", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarCategoriaProductoAsync(int idCategoria, string nombre, string? descripcion) => EjecutarAsync("ActualizarCategoriaProducto",
        ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre), ("p_descripcion", null, descripcion));

    public Task DesactivarCategoriaProductoAsync(int idCategoria) => EjecutarAsync("DesactivarCategoriaProducto", ("p_id_categoria", null, idCategoria));

    // PRODUCTOS
    public Task<int> RegistrarProductoAsync(Producto p) => EjecutarIdAsync("RegistrarProducto",
        ("p_id_categoria", null, p.IdCategoriaProducto), ("p_codigo", null, p.Codigo), ("p_nombre", null, p.Nombre),
        ("p_descripcion", null, p.Descripcion), ("p_precio_costo", null, p.PrecioCosto), ("p_precio_venta", null, p.PrecioVenta),
        ("p_stock", null, p.StockActual), ("p_stock_minimo", null, p.StockMinimo));

    public async Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ProductoConsulta>("ObtenerProductos", commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int idProducto)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ProductoConsulta>("ObtenerProductoPorId", new { p_id_producto = idProducto }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarProductoAsync(int idProducto, int idCategoria, string codigo, string nombre, string? descripcion, decimal precioCosto, decimal precioVenta, int stockMinimo) => EjecutarAsync("ActualizarProducto",
        ("p_id_producto", null, idProducto), ("p_id_categoria", null, idCategoria), ("p_codigo", null, codigo),
        ("p_nombre", null, nombre), ("p_descripcion", null, descripcion), ("p_precio_costo", null, precioCosto),
        ("p_precio_venta", null, precioVenta), ("p_stock_minimo", null, stockMinimo));

    public Task DesactivarProductoAsync(int idProducto) => EjecutarAsync("DesactivarProducto", ("p_id_producto", null, idProducto));

    // STOCK
    public Task<int> ActualizarStockProductoAsync(int id, int cantidad, string tipo, string? motivo) => EjecutarIdAsync("ActualizarStockProducto",
        ("p_id_producto", null, id), ("p_cantidad", null, cantidad), ("p_tipo_movimiento", null, tipo), ("p_motivo", null, motivo));

    public async Task<IEnumerable<MovimientoStockConsulta>> ObtenerMovimientosStockAsync(int idProducto)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = "SELECT id_movimiento as IdMovimiento, id_producto as IdProducto, tipo_movimiento as TipoMovimiento, cantidad as Cantidad, fecha_hora as FechaHora, motivo as Motivo, stock_anterior as StockAnterior, stock_posterior as StockPosterior FROM movimiento_stock WHERE id_producto = @id";
        return await conn.QueryAsync<MovimientoStockConsulta>(sql, new { id = idProducto });
    }

    // COMPRAS
    public Task<int> RegistrarCompraAsync(int ip, int i, int c, decimal pc, string? o) => EjecutarIdAsync("RegistrarCompra",
        ("p_id_proveedor", null, ip), ("p_id_producto", null, i), ("p_cantidad", null, c), ("p_precio_costo", null, pc), ("p_observacion", null, o));

    public async Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CompraConsulta>("ObtenerCompras", commandType: CommandType.StoredProcedure);
    }

    public async Task<CompraConsulta?> ObtenerCompraPorIdAsync(int idCompra)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<CompraConsulta>("ObtenerCompraPorId", new { p_id_compra = idCompra }, commandType: CommandType.StoredProcedure);
    }

    // CATEGORIAS SERVICIO
    public async Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand(@"INSERT INTO Categorias_Servicio (nombre, descripcion, activo) VALUES (@nombre, @descripcion, TRUE);", c);
        cmd.Parameters.AddWithValue("@nombre", nombre);
        cmd.Parameters.AddWithValue("@descripcion", descripcion ?? (object)DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
        using var cmdId = new MySqlCommand("SELECT LAST_INSERT_ID();", c);
        return Convert.ToInt32(await cmdId.ExecuteScalarAsync());
    }

    public async Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<CategoriaServicio>("ObtenerCategoriasServicio", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarCategoriaServicioAsync(int idCategoria, string nombre, string? descripcion) => EjecutarAsync("ActualizarCategoriaServicio",
        ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre), ("p_descripcion", null, descripcion));

    public Task DesactivarCategoriaServicioAsync(int idCategoria) => EjecutarAsync("DesactivarCategoriaServicio", ("p_id_categoria", null, idCategoria));

    // SERVICIOS
    public Task<int> RegistrarServicioAsync(Servicio s) => EjecutarIdAsync("RegistrarServicio",
        ("p_id_categoria", null, s.IdCategoriaServicio), ("p_nombre", null, s.Nombre), ("p_descripcion", null, s.Descripcion),
        ("p_precio", null, s.Precio), ("p_duracion", null, s.DuracionMinuto));

    public async Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ServicioConsulta>("ObtenerServicios", commandType: CommandType.StoredProcedure);
    }

    public async Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int idServicio)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<ServicioConsulta>("ObtenerServicioPorId", new { p_id_servicio = idServicio }, commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarServicioAsync(int idServicio, int idCategoria, string nombre, string? descripcion, decimal precio, int? duracion) => EjecutarAsync("ActualizarServicio",
        ("p_id_servicio", null, idServicio), ("p_id_categoria", null, idCategoria), ("p_nombre", null, nombre),
        ("p_descripcion", null, descripcion), ("p_precio", null, precio), ("p_duracion", null, duracion));

    public Task DesactivarServicioAsync(int idServicio) => EjecutarAsync("DesactivarServicio", ("p_id_servicio", null, idServicio));

    // TURNOS
    public Task<int> CrearTurnoAsync(Turno t) => EjecutarIdAsync("CrearTurno",
        ("p_id_cliente", null, t.IdCliente), ("p_id_empleado", null, t.IdEmpleado), ("p_fecha", null, t.Fecha.Date),
        ("p_hora", null, t.Hora), ("p_observacion", null, t.Observacion));

    public Task<int> AgregarServicioATurnoAsync(int it, int is_, decimal d) => EjecutarIdAsync("AgregarServicioATurno",
        ("p_id_turno", null, it), ("p_id_servicio", null, is_), ("p_descuento", null, d));

    public async Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<TurnoConsulta>("ObtenerTurnos", commandType: CommandType.StoredProcedure);
    }

    public async Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int idTurno)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = @"SELECT t.id_turno as IdTurno, t.id_cliente as IdCliente, CONCAT(pc.nombre, ' ', pc.apellido) as NombreCliente, 
                           t.id_empleado as IdEmpleado, CONCAT(pe.nombre, ' ', pe.apellido) as NombreEmpleado, 
                           t.fecha as Fecha, t.hora as Hora, t.estado as Estado, t.observacion as Observacion
                    FROM turnos t
                    INNER JOIN clientes c ON t.id_cliente = c.id_cliente
                    INNER JOIN personas pc ON c.id_persona = pc.id_persona
                    INNER JOIN empleados e ON t.id_empleado = e.id_empleado
                    INNER JOIN personas pe ON e.id_persona = pe.id_persona
                    WHERE t.id_turno = @id";
        return await conn.QueryFirstOrDefaultAsync<TurnoConsulta>(sql, new { id = idTurno });
    }

    public Task CambiarEstadoTurnoAsync(int idTurno, string estado) => EjecutarAsync("CambiarEstadoTurno", ("p_id_turno", null, idTurno), ("p_estado", null, estado));

    // VENTAS
    public Task<int> CrearVentaDesdeTurnoAsync(int idTurno, decimal descuento, string? observacion) => EjecutarIdAsync("CrearVentaDesdeTurno",
        ("p_id_turno", null, idTurno), ("p_descuento", null, descuento), ("p_observacion", null, observacion));

    public Task<int> RealizarVentaProductoAsync(int ic, int ip, int c) => EjecutarIdAsync("RealizarVentaProducto",
        ("p_id_cliente", null, ic), ("p_id_producto", null, ip), ("p_cantidad", null, c));

    public async Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<VentaConsulta>("ObtenerVentas", commandType: CommandType.StoredProcedure);
    }

    public async Task<VentaConsulta?> ObtenerVentaPorIdAsync(int idVenta)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        var sql = @"SELECT v.id_venta as IdVenta, v.id_cliente as IdCliente, CONCAT(p.nombre, ' ', p.apellido) as NombreCliente,
                           v.fecha_hora as FechaHora, v.tipo_venta as TipoVenta, v.estado as Estado, v.subtotal as Subtotal, v.descuento as Descuento, v.total as Total, v.observacion as Observacion
                    FROM ventas v
                    INNER JOIN clientes c ON v.id_cliente = c.id_cliente
                    INNER JOIN personas p ON c.id_persona = p.id_persona
                    WHERE v.id_venta = @id";
        return await conn.QueryFirstOrDefaultAsync<VentaConsulta>(sql, new { id = idVenta });
    }

    // PAGOS
    public Task<int> RegistrarPagoAsync(Pago p) => EjecutarIdAsync("RegistrarPago",
        ("p_id_venta", null, p.IdVenta), ("p_monto", null, p.Monto), ("p_metodo_pago", null, p.MetodoPago), ("p_referencia", null, p.Referencia));

    public async Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<PagoConsulta>("ObtenerPagosPorVenta", new { p_id_venta = idVenta }, commandType: CommandType.StoredProcedure);
    }

    // FACTURAS
    public Task<int> GenerarFacturaAsync(Factura f) => EjecutarIdAsync("GenerarFactura",
        ("p_id_venta", null, f.IdVenta), ("p_numero_factura", null, f.NumeroFactura), ("p_tipo_comprobante", null, f.TipoComprobante));

    public async Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<FacturaConsulta>("ObtenerFacturas", commandType: CommandType.StoredProcedure);
    }

    public async Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int idFactura)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<FacturaConsulta>("ObtenerFacturaPorId", new { p_id_factura = idFactura }, commandType: CommandType.StoredProcedure);
    }

    // MEMBRESIAS
    public Task<int> CrearMembresiaAsync(Membresia m) => EjecutarIdAsync("CrearMembresia",
        ("p_nombre_plan", null, m.NombrePlan), ("p_descripcion", null, m.Descripcion), ("p_costo", null, m.Costo),
        ("p_duracion_dias", null, m.DuracionDias), ("p_descuento", null, m.PorcentajeDescuento));

    public Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm) => EjecutarIdAsync("AsignarMembresiaCliente",
        ("p_id_cliente", null, cm.IdCliente), ("p_id_membresia", null, cm.IdMembresia), ("p_observacion", null, cm.Observacion));

    public async Task<IEnumerable<Membresia>> ObtenerMembresiasAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<Membresia>("ObtenerMembresias", commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente)
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryAsync<ClienteMembresiaConsulta>("ObtenerClienteMembresias", new { p_id_cliente = idCliente }, commandType: CommandType.StoredProcedure);
    }

    public Task RegistrarPagoMembresiaAsync(int idClienteMembresia, decimal monto, string metodoPago, string referencia) => EjecutarAsync("RegistrarPagoMembresia",
        ("p_id_cliente_membresia", null, idClienteMembresia), ("p_monto", null, monto), ("p_metodo", null, metodoPago), ("p_referencia", null, referencia));

    // SALON
    public async Task<Salon?> ObtenerSalonAsync()
    {
        using var conn = await _conn.ObtenerConexionAsync();
        return await conn.QueryFirstOrDefaultAsync<Salon>("ObtenerSalon", commandType: CommandType.StoredProcedure);
    }

    public Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo) => EjecutarAsync("ActualizarSalon",
        ("p_id_salon", null, idSalon), ("p_nombre", null, nombre), ("p_direccion", null, direccion),
        ("p_telefono", null, telefono), ("p_email", null, email), ("p_logo", null, logo));
}