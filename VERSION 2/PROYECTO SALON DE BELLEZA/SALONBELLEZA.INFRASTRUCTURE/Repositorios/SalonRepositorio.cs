using MySqlConnector;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
using System.Data;
namespace SALONBELLEZA.INFRASTRUCTURE.Repositorios;
public class SalonRepositorio : ISalonRepositorio
{
    private readonly IMyConexion _conn;
    public SalonRepositorio(IMyConexion conn) => _conn=conn;

    private async Task<int> EjecutarIdAsync(string proc, params (string,string?,object?)[] ps)
    {
        using var c=(MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd=new MySqlCommand(proc,c){CommandType=CommandType.StoredProcedure};
        foreach(var p in ps) cmd.Parameters.AddWithValue(p.Item1,p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
        using var q=new MySqlCommand("SELECT LAST_INSERT_ID();",c);
        var v=await q.ExecuteScalarAsync();
        return Convert.ToInt32(v);
    }
    private async Task EjecutarAsync(string proc, params (string,string?,object?)[] ps)
    {
        using var c=(MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd=new MySqlCommand(proc,c){CommandType=CommandType.StoredProcedure};
        foreach(var p in ps) cmd.Parameters.AddWithValue(p.Item1,p.Item3 ?? DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }

    public Task<int> RegistrarClienteAsync(Persona p,string? cat)=>EjecutarIdAsync("RegistrarCliente",
        ("p_dni",null,p.Dni),("p_nombre",null,p.Nombre),("p_apellido",null,p.Apellido),("p_telefono",null,p.Telefono),
        ("p_email",null,p.Email),("p_direccion",null,p.Direccion),("p_categoria",null,cat));
    public Task<int> RegistrarEmpleadoAsync(Persona p,string? tipo)=>EjecutarIdAsync("RegistrarEmpleado",
        ("p_dni",null,p.Dni),("p_nombre",null,p.Nombre),("p_apellido",null,p.Apellido),("p_telefono",null,p.Telefono),
        ("p_email",null,p.Email),("p_direccion",null,p.Direccion),("p_tipo_empleado",null,tipo));
    public Task<int> RegistrarCategoriaProductoAsync(string n,string? d)=>EjecutarIdAsync("RegistrarCategoriaProducto",("p_nombre",null,n),("p_descripcion",null,d));

    public async Task<int> RegistrarCategoriaServicioAsync(
    string nombre,
    string? descripcion)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            @"INSERT INTO Categorias_Servicio
          (nombre, descripcion, activo)
          VALUES
          (@nombre, @descripcion, TRUE);",
            c);

        cmd.Parameters.AddWithValue("@nombre", nombre);
        cmd.Parameters.AddWithValue(
            "@descripcion",
            descripcion ?? (object)DBNull.Value);

        await cmd.ExecuteNonQueryAsync();

        using var cmdId = new MySqlCommand(
            "SELECT LAST_INSERT_ID();",
            c);

        return Convert.ToInt32(await cmdId.ExecuteScalarAsync());
    }
    public Task<int> RegistrarProductoAsync(Producto p)=>EjecutarIdAsync("RegistrarProducto",
        ("p_id_categoria",null,p.IdCategoriaProducto),("p_codigo",null,p.Codigo),("p_nombre",null,p.Nombre),
        ("p_descripcion",null,p.Descripcion),("p_precio_costo",null,p.PrecioCosto),("p_precio_venta",null,p.PrecioVenta),
        ("p_stock",null,p.StockActual),("p_stock_minimo",null,p.StockMinimo));
    public Task<int> ActualizarStockProductoAsync(int id,int cantidad,string tipo,string? motivo)=>EjecutarIdAsync("ActualizarStockProducto",
        ("p_id_producto",null,id),("p_cantidad",null,cantidad),("p_tipo_movimiento",null,tipo),("p_motivo",null,motivo));
    public Task<int> RegistrarProveedorAsync(Proveedor p)=>EjecutarIdAsync("RegistrarProveedor",
        ("p_razon_social",null,p.RazonSocial),("p_cuit",null,p.Cuit),("p_telefono",null,p.Telefono),("p_email",null,p.Email),("p_direccion",null,p.Direccion));
    public Task<int> RegistrarCompraAsync(int ip,int i,int c,decimal pc,string? o)=>EjecutarIdAsync("RegistrarCompra",
        ("p_id_proveedor",null,ip),("p_id_producto",null,i),("p_cantidad",null,c),("p_precio_costo",null,pc),("p_observacion",null,o));
    public Task<int> RegistrarServicioAsync(Servicio s)=>EjecutarIdAsync("RegistrarServicio",
        ("p_id_categoria",null,s.IdCategoriaServicio),("p_nombre",null,s.Nombre),("p_descripcion",null,s.Descripcion),
        ("p_precio",null,s.Precio),("p_duracion",null,s.DuracionMinuto));
    public Task<int> CrearTurnoAsync(Turno t)=>EjecutarIdAsync("CrearTurno",
        ("p_id_cliente",null,t.IdCliente),("p_id_empleado",null,t.IdEmpleado),("p_fecha",null,t.Fecha.Date),
        ("p_hora",null,t.Hora),("p_observacion",null,t.Observacion));
    public Task<int> AgregarServicioATurnoAsync(int it,int is_,decimal d)=>EjecutarIdAsync("AgregarServicioATurno",
        ("p_id_turno",null,it),("p_id_servicio",null,is_),("p_descuento",null,d));

    public Task<int> CrearVentaDesdeTurnoAsync(
    int idTurno,
    decimal descuento,
    string? observacion)
    => EjecutarIdAsync(
        "CrearVentaDesdeTurno",
        ("p_id_turno", null, idTurno),
        ("p_descuento", null, descuento),
        ("p_observacion", null, observacion));

    public Task<int> RealizarVentaProductoAsync(int ic,int ip,int c)=>EjecutarIdAsync("RealizarVentaProducto",
        ("p_id_cliente",null,ic),("p_id_producto",null,ip),("p_cantidad",null,c));
    public Task<int> RegistrarPagoAsync(Pago p)=>EjecutarIdAsync("RegistrarPago",
        ("p_id_venta",null,p.IdVenta),("p_monto",null,p.Monto),("p_metodo_pago",null,p.MetodoPago),("p_referencia",null,p.Referencia));
    public Task<int> GenerarFacturaAsync(Factura f)=>EjecutarIdAsync("GenerarFactura",
        ("p_id_venta",null,f.IdVenta),("p_numero_factura",null,f.NumeroFactura),("p_tipo_comprobante",null,f.TipoComprobante));
    public Task<int> CrearMembresiaAsync(Membresia m)=>EjecutarIdAsync("CrearMembresia",
        ("p_nombre_plan",null,m.NombrePlan),("p_descripcion",null,m.Descripcion),("p_costo",null,m.Costo),
        ("p_duracion_dias",null,m.DuracionDias),("p_descuento",null,m.PorcentajeDescuento));
    public Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm)=>EjecutarIdAsync("AsignarMembresiaCliente",
        ("p_id_cliente",null,cm.IdCliente),("p_id_membresia",null,cm.IdMembresia),("p_observacion",null,cm.Observacion));
}