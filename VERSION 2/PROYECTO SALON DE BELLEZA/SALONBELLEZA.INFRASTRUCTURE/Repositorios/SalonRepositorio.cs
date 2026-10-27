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


    // ===============================
    // CONSULTAS Y OPERACIONES DE SERVICIOS
    // ===============================

    public async Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync()
    {
        var lista = new List<ServicioConsulta>();

        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ObtenerServicios", c);
        cmd.CommandType = CommandType.StoredProcedure;

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(MapearServicioConsulta(reader));
        }

        return lista;
    }

    public async Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int idServicio)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ObtenerServicioPorId", c);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("p_id_servicio", idServicio);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapearServicioConsulta(reader);
        }

        return null;
    }

    public Task ActualizarServicioAsync(
        int idServicio,
        int idCategoriaServicio,
        string nombre,
        string? descripcion,
        decimal precio,
        int? duracionMinuto)
    {
        return EjecutarAsync(
            "ActualizarServicio",
            ("p_id_servicio", null, (object)idServicio),
            ("p_id_categoria", null, (object)idCategoriaServicio),
            ("p_nombre", null, (object)nombre),
            ("p_descripcion", null, (object?)descripcion),
            ("p_precio", null, (object)precio),
            ("p_duracion", null, (object?)duracionMinuto)
        );
    }

    public Task DesactivarServicioAsync(int idServicio)
    {
        return EjecutarAsync(
            "DesactivarServicio",
            ("p_id_servicio", null, (object)idServicio)
        );
    }

    private static ServicioConsulta MapearServicioConsulta(
        MySqlDataReader reader)
    {
        return new ServicioConsulta
        {
            IdServicio = Convert.ToInt32(reader["id_servicio"]),
            IdCategoriaServicio =
                Convert.ToInt32(reader["id_categoria_servicio"]),
            NombreCategoria = reader["nombre_categoria"] == DBNull.Value
                ? null
                : Convert.ToString(reader["nombre_categoria"]),
            Nombre = Convert.ToString(reader["nombre"]) ?? string.Empty,
            Descripcion = reader["descripcion"] == DBNull.Value
                ? null
                : Convert.ToString(reader["descripcion"]),
            Precio = Convert.ToDecimal(reader["precio"]),
            DuracionMinuto = reader["duracion_minuto"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["duracion_minuto"]),
            Estado = reader["estado"] == DBNull.Value
                ? null
                : Convert.ToString(reader["estado"])
        };
    }





    public Task<int> CrearTurnoAsync(Turno t)=>EjecutarIdAsync("CrearTurno",
        ("p_id_cliente",null,t.IdCliente),("p_id_empleado",null,t.IdEmpleado),("p_fecha",null,t.Fecha.Date),
        ("p_hora",null,t.Hora),("p_observacion",null,t.Observacion));



    // ===============================
    // CONSULTAS Y OPERACIONES DE TURNOS
    // ===============================

    public async Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync()
    {
        var lista = new List<TurnoConsulta>();

        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ObtenerTurnos", c);

        cmd.CommandType = CommandType.StoredProcedure;

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            lista.Add(new TurnoConsulta
            {
                IdTurno = Convert.ToInt32(reader["id_turno"]),
                IdCliente = Convert.ToInt32(reader["id_cliente"]),
                NombreCliente = Convert.ToString(reader["nombre_cliente"]),
                IdEmpleado = Convert.ToInt32(reader["id_empleado"]),
                NombreEmpleado = Convert.ToString(reader["nombre_empleado"]),
                Fecha = Convert.ToDateTime(reader["fecha"]),
                Hora = TimeSpan.Parse(reader["hora"].ToString()!),
                Estado = reader["estado"] == DBNull.Value
                    ? null
                    : Convert.ToString(reader["estado"]),
                Observacion = reader["observacion"] == DBNull.Value
                    ? null
                    : Convert.ToString(reader["observacion"])
            });
        }

        return lista;
    }

    public Task CambiarEstadoTurnoAsync(int idTurno, string estado)
    {
        return EjecutarAsync(
            "CambiarEstadoTurno",
            ("p_id_turno", null, (object)idTurno),
            ("p_estado", null, (object)estado)
        );
    }



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

    public async Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync()
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            "ObtenerClientes",
            c)
        {
            CommandType = CommandType.StoredProcedure
        };

        using var reader = await cmd.ExecuteReaderAsync();

        var clientes = new List<ClienteConsulta>();

        while (await reader.ReadAsync())
        {
            clientes.Add(new ClienteConsulta
            {
                IdCliente = reader.GetInt32("id_cliente"),
                IdPersona = reader.GetInt32("id_persona"),

                Dni = reader.IsDBNull(reader.GetOrdinal("dni"))
                    ? null
                    : reader.GetString("dni"),

                Nombre = reader.GetString("nombre"),
                Apellido = reader.GetString("apellido"),

                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono"))
                    ? null
                    : reader.GetString("telefono"),

                Email = reader.IsDBNull(reader.GetOrdinal("email"))
                    ? null
                    : reader.GetString("email"),

                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))
                    ? null
                    : reader.GetString("direccion"),

                CategoriaCliente = reader.IsDBNull(reader.GetOrdinal("categoria_cliente"))
                    ? null
                    : reader.GetString("categoria_cliente"),

                FechaRegistro = reader.IsDBNull(reader.GetOrdinal("fecha_registro"))
                    ? null
                    : reader.GetDateTime("fecha_registro"),

                Estado = reader.IsDBNull(reader.GetOrdinal("estado"))
                    ? null
                    : reader.GetString("estado")
            });
        }

        return clientes;
    }


    public async Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            "ObtenerClientePorId",
            c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_cliente", idCliente);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new ClienteConsulta
            {
                IdCliente = reader.GetInt32("id_cliente"),
                IdPersona = reader.GetInt32("id_persona"),

                Dni = reader.IsDBNull(reader.GetOrdinal("dni"))
                    ? null
                    : reader.GetString("dni"),

                Nombre = reader.GetString("nombre"),
                Apellido = reader.GetString("apellido"),

                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono"))
                    ? null
                    : reader.GetString("telefono"),

                Email = reader.IsDBNull(reader.GetOrdinal("email"))
                    ? null
                    : reader.GetString("email"),

                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))
                    ? null
                    : reader.GetString("direccion"),

                CategoriaCliente = reader.IsDBNull(reader.GetOrdinal("categoria_cliente"))
                    ? null
                    : reader.GetString("categoria_cliente"),

                FechaRegistro = reader.IsDBNull(reader.GetOrdinal("fecha_registro"))
                    ? null
                    : reader.GetDateTime("fecha_registro"),

                Estado = reader.IsDBNull(reader.GetOrdinal("estado"))
                    ? null
                    : reader.GetString("estado")
            };
        }

        return null;
    }



    public async Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            "BuscarClientes",
            c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_termino", termino);

        using var reader = await cmd.ExecuteReaderAsync();

        var clientes = new List<ClienteConsulta>();

        while (await reader.ReadAsync())
        {
            clientes.Add(new ClienteConsulta
            {
                IdCliente = reader.GetInt32("id_cliente"),
                IdPersona = reader.GetInt32("id_persona"),

                Dni = reader.IsDBNull(reader.GetOrdinal("dni"))
                    ? null
                    : reader.GetString("dni"),

                Nombre = reader.GetString("nombre"),
                Apellido = reader.GetString("apellido"),

                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono"))
                    ? null
                    : reader.GetString("telefono"),

                Email = reader.IsDBNull(reader.GetOrdinal("email"))
                    ? null
                    : reader.GetString("email"),

                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))
                    ? null
                    : reader.GetString("direccion"),

                CategoriaCliente = reader.IsDBNull(reader.GetOrdinal("categoria_cliente"))
                    ? null
                    : reader.GetString("categoria_cliente"),

                FechaRegistro = reader.IsDBNull(reader.GetOrdinal("fecha_registro"))
                    ? null
                    : reader.GetDateTime("fecha_registro"),

                Estado = reader.IsDBNull(reader.GetOrdinal("estado"))
                    ? null
                    : reader.GetString("estado")
            });
        }

        return clientes;
    }


    public async Task ActualizarClienteAsync(
    int idCliente,
    string? dni,
    string nombre,
    string apellido,
    string? telefono,
    string? email,
    string? direccion)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            "ActualizarCliente",
            c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_cliente", idCliente);
        cmd.Parameters.AddWithValue("@p_dni", dni ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_nombre", nombre);
        cmd.Parameters.AddWithValue("@p_apellido", apellido);
        cmd.Parameters.AddWithValue("@p_telefono", telefono ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_email", email ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_direccion", direccion ?? (object)DBNull.Value);

        await cmd.ExecuteNonQueryAsync();
    }


    public async Task DesactivarClienteAsync(int idCliente)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();

        using var cmd = new MySqlCommand(
            "DesactivarCliente",
            c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_cliente", idCliente);

        await cmd.ExecuteNonQueryAsync();
    }



    // ===============================
    // MÉTODOS DE EMPLEADOS
    // ===============================

    public async Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync()
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ObtenerEmpleados", c)
        {
            CommandType = CommandType.StoredProcedure
        };

        using var reader = await cmd.ExecuteReaderAsync();
        var empleados = new List<EmpleadoConsulta>();

        while (await reader.ReadAsync())
        {
            empleados.Add(MapearEmpleado(reader));
        }

        return empleados;
    }

    public async Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int idEmpleado)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ObtenerEmpleadoPorId", c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_empleado", idEmpleado);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapearEmpleado(reader);
        }

        return null;
    }

    public async Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("BuscarEmpleados", c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_termino", termino);

        using var reader = await cmd.ExecuteReaderAsync();
        var empleados = new List<EmpleadoConsulta>();

        while (await reader.ReadAsync())
        {
            empleados.Add(MapearEmpleado(reader));
        }

        return empleados;
    }

    public async Task ActualizarEmpleadoAsync(
        int idEmpleado,
        string? dni,
        string nombre,
        string apellido,
        string? telefono,
        string? email,
        string? direccion,
        string? tipoEmpleado)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("ActualizarEmpleado", c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_empleado", idEmpleado);
        cmd.Parameters.AddWithValue("@p_dni", dni ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_nombre", nombre);
        cmd.Parameters.AddWithValue("@p_apellido", apellido);
        cmd.Parameters.AddWithValue("@p_telefono", telefono ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_email", email ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_direccion", direccion ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@p_tipo_empleado", tipoEmpleado ?? (object)DBNull.Value);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DesactivarEmpleadoAsync(int idEmpleado)
    {
        using var c = (MySqlConnection)await _conn.ObtenerConexionAsync();
        using var cmd = new MySqlCommand("DesactivarEmpleado", c)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@p_id_empleado", idEmpleado);

        await cmd.ExecuteNonQueryAsync();
    }

    private static EmpleadoConsulta MapearEmpleado(MySqlDataReader reader)
    {
        return new EmpleadoConsulta
        {
            IdEmpleado = reader.GetInt32("id_empleado"),
            IdPersona = reader.GetInt32("id_persona"),
            Dni = reader.IsDBNull(reader.GetOrdinal("dni"))
                ? null : reader.GetString("dni"),
            Nombre = reader.GetString("nombre"),
            Apellido = reader.GetString("apellido"),
            Telefono = reader.IsDBNull(reader.GetOrdinal("telefono"))
                ? null : reader.GetString("telefono"),
            Email = reader.IsDBNull(reader.GetOrdinal("email"))
                ? null : reader.GetString("email"),
            Direccion = reader.IsDBNull(reader.GetOrdinal("direccion"))
                ? null : reader.GetString("direccion"),
            TipoEmpleado = reader.IsDBNull(reader.GetOrdinal("tipo_empleado"))
                ? null : reader.GetString("tipo_empleado"),
            FechaIngreso = reader.IsDBNull(reader.GetOrdinal("fecha_ingreso"))
                ? null : reader.GetDateTime("fecha_ingreso"),
            Estado = reader.IsDBNull(reader.GetOrdinal("estado"))
                ? null : reader.GetString("estado")
        };
    }


}





