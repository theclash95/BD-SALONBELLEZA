using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
namespace SALONBELLEZA.CORE.Servicios;
public class ServicioSalon
{
    private readonly ISalonRepositorio _repo;
    public ServicioSalon(ISalonRepositorio repo) => _repo = repo;

    //metodos clientes
    public Task<int> RegistrarClienteAsync(Persona p,string? categoria)
    {
        ValidarPersona(p);
        return _repo.RegistrarClienteAsync(p,categoria);
    }
    //metodo empleados
    public Task<IEnumerable<EmpleadoConsulta>> ObtenerEmpleadosAsync() => _repo.ObtenerEmpleadosAsync();
    public Task<EmpleadoConsulta?> ObtenerEmpleadoPorIdAsync(int id) => _repo.ObtenerEmpleadoPorIdAsync(id);
    public Task<IEnumerable<EmpleadoConsulta>> BuscarEmpleadosAsync(string termino) => _repo.BuscarEmpleadosAsync(termino);
    public Task ActualizarEmpleadoAsync(int id, string? dni, string nombre, string apellido, string? tel, string? email, string? dir, string? tipo)
        => _repo.ActualizarEmpleadoAsync(id, dni, nombre, apellido, tel, email, dir, tipo);
    public Task DesactivarEmpleadoAsync(int id) => _repo.DesactivarEmpleadoAsync(id);
    public Task<int> RegistrarEmpleadoAsync(Persona p,string? tipo)
    {
        ValidarPersona(p);
        return _repo.RegistrarEmpleadoAsync(p,tipo);
    }
    // metodos Productos
    public Task<IEnumerable<ProductoConsulta>> ObtenerProductosAsync() => _repo.ObtenerProductosAsync();
    public Task<ProductoConsulta?> ObtenerProductoPorIdAsync(int id) => _repo.ObtenerProductoPorIdAsync(id);
    public Task ActualizarProductoAsync(int id, int idCat, string cod, string nom, string? desc, decimal pCosto, decimal pVenta, int stkMin)
        => _repo.ActualizarProductoAsync(id, idCat, cod, nom, desc, pCosto, pVenta, stkMin);
    public Task DesactivarProductoAsync(int id) => _repo.DesactivarProductoAsync(id);

    public Task<int> RegistrarCategoriaProductoAsync(string n,string? d)
    {
        if(string.IsNullOrWhiteSpace(n)) throw new ArgumentException("El nombre es obligatorio.");
        return _repo.RegistrarCategoriaProductoAsync(n,d);
    }
    //metodos servicios
    public Task<IEnumerable<ServicioConsulta>> ObtenerServiciosAsync() => _repo.ObtenerServiciosAsync();
    public Task<ServicioConsulta?> ObtenerServicioPorIdAsync(int id) => _repo.ObtenerServicioPorIdAsync(id);
    public Task ActualizarServicioAsync(int id, int idCat, string nom, string? desc, decimal precio, int? dur)
        => _repo.ActualizarServicioAsync(id, idCat, nom, desc, precio, dur);
    public Task DesactivarServicioAsync(int id) => _repo.DesactivarServicioAsync(id);
    public Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría de servicio es obligatorio.");

        return _repo.RegistrarCategoriaServicioAsync(nombre, descripcion);
    }
    //metodo Producto
    public Task<int> RegistrarProductoAsync(Producto p)
    {
        if(p.IdCategoriaProducto<=0 || string.IsNullOrWhiteSpace(p.Nombre) || p.PrecioVenta<0 || p.StockActual<0)
            throw new ArgumentException("Datos de producto inválidos.");
        return _repo.RegistrarProductoAsync(p);
    }
    public Task<int> ActualizarStockProductoAsync(int id,int cantidad,string tipo,string? motivo)
    {
        if(id<=0 || cantidad<=0) throw new ArgumentException("Producto y cantidad deben ser válidos.");
        if(!tipo.Equals("INGRESO",StringComparison.OrdinalIgnoreCase) && !tipo.Equals("EGRESO",StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("El tipo de movimiento debe ser INGRESO o EGRESO.");
        return _repo.ActualizarStockProductoAsync(id,cantidad,tipo.ToUpperInvariant(),motivo);
    }
    //metodo de proveedor
    public Task<int> RegistrarProveedorAsync(Proveedor p)
    {
        if(string.IsNullOrWhiteSpace(p.RazonSocial)) throw new ArgumentException("La razón social es obligatoria.");
        return _repo.RegistrarProveedorAsync(p);
    }

    public Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync()
    {
        return _repo.ObtenerProveedoresAsync();
    }

    public Task<Proveedor?> ObtenerProveedorPorIdAsync(int idProveedor)
    {
        if (idProveedor <= 0)
            throw new ArgumentException("El ID del proveedor debe ser mayor a 0.");

        return _repo.ObtenerProveedorPorIdAsync(idProveedor);
    }

    public Task ActualizarProveedorAsync(int idProveedor, string razonSocial, string cuit, string telefono, string email, string direccion)
    {
        if (idProveedor <= 0)
            throw new ArgumentException("El ID del proveedor es inválido.");
        if (string.IsNullOrWhiteSpace(razonSocial))
            throw new ArgumentException("La razón social es obligatoria.");

        return _repo.ActualizarProveedorAsync(idProveedor, razonSocial, cuit, telefono, email, direccion);
    }

    public Task DesactivarProveedorAsync(int idProveedor)
    {
        if (idProveedor <= 0)
            throw new ArgumentException("El ID del proveedor es inválido.");

        return _repo.DesactivarProveedorAsync(idProveedor);
    }
    // MÉTODOS PARA COMPRAS
    public Task<IEnumerable<CompraConsulta>> ObtenerComprasAsync()
    {
        return _repo.ObtenerComprasAsync();
    }

    public Task<CompraConsulta?> ObtenerCompraPorIdAsync(int idCompra)
    {
        if (idCompra <= 0) throw new ArgumentException("El ID de compra es inválido.");
        return _repo.ObtenerCompraPorIdAsync(idCompra);
    }
    public Task<int> RegistrarCompraAsync(int idProv,int idProd,int cant,decimal costo,string? obs)
    {
        if(idProv<=0||idProd<=0||cant<=0||costo<0) throw new ArgumentException("Datos de compra inválidos.");
        return _repo.RegistrarCompraAsync(idProv,idProd,cant,costo,obs);
    }
    public Task<int> RegistrarServicioAsync(Servicio s)
    {
        if(s.IdCategoriaServicio<=0||string.IsNullOrWhiteSpace(s.Nombre)||s.Precio<0) throw new ArgumentException("Datos de servicio inválidos.");
        return _repo.RegistrarServicioAsync(s);
    }
    //metodo turno
    public Task<IEnumerable<TurnoConsulta>> ObtenerTurnosAsync() => _repo.ObtenerTurnosAsync();
    public Task<TurnoConsulta?> ObtenerTurnoPorIdAsync(int id) => _repo.ObtenerTurnoPorIdAsync(id);
    public Task CambiarEstadoTurnoAsync(int id, string estado) => _repo.CambiarEstadoTurnoAsync(id, estado);
    public Task<int> CrearTurnoAsync(Turno t)
    {
        if(t.IdCliente<=0||t.IdEmpleado<=0) throw new ArgumentException("Cliente y empleado son obligatorios.");
        return _repo.CrearTurnoAsync(t);
    }
    public Task<int> AgregarServicioATurnoAsync(int idTurno,int idServicio,decimal descuento)
    {
        if(idTurno<=0||idServicio<=0||descuento<0) throw new ArgumentException("Datos de detalle de turno inválidos.");
        return _repo.AgregarServicioATurnoAsync(idTurno,idServicio,descuento);
    }
    // MÉTODOS PARA VENTAS
    public Task<IEnumerable<VentaConsulta>> ObtenerVentasAsync()
    {
        return _repo.ObtenerVentasAsync();
    }

    public Task<VentaConsulta?> ObtenerVentaPorIdAsync(int idVenta)
    {
        if (idVenta <= 0) throw new ArgumentException("El ID de venta es inválido.");
        return _repo.ObtenerVentaPorIdAsync(idVenta);
    }
    public Task<int> RealizarVentaProductoAsync(int idCliente,int idProducto,int cantidad)
    {
        if(idCliente<=0||idProducto<=0||cantidad<=0) throw new ArgumentException("Datos de venta inválidos.");
        return _repo.RealizarVentaProductoAsync(idCliente,idProducto,cantidad);
    }
    //metodos pagos
    public Task<IEnumerable<PagoConsulta>> ObtenerPagosPorVentaAsync(int idVenta)
    {
        if (idVenta <= 0)
            throw new ArgumentException("El ID de la venta es inválido.");

        return _repo.ObtenerPagosPorVentaAsync(idVenta);
    }
    public Task<int> RegistrarPagoAsync(Pago p)
    {
        if(p.IdVenta<=0||p.Monto is null||p.Monto<0) throw new ArgumentException("Datos de pago inválidos.");
        return _repo.RegistrarPagoAsync(p);
    }
    //metodos factura
    public Task<IEnumerable<FacturaConsulta>> ObtenerFacturasAsync()
    {
        return _repo.ObtenerFacturasAsync();
    }

    public Task<FacturaConsulta?> ObtenerFacturaPorIdAsync(int idFactura)
    {
        if (idFactura <= 0)
            throw new ArgumentException("El ID de la factura es inválido.");

        return _repo.ObtenerFacturaPorIdAsync(idFactura);
    }

    public Task<int> GenerarFacturaAsync(Factura f)
    {
        if(f.IdVenta<=0||string.IsNullOrWhiteSpace(f.NumeroFactura)||string.IsNullOrWhiteSpace(f.TipoComprobante))
            throw new ArgumentException("Datos de factura inválidos.");
        return _repo.GenerarFacturaAsync(f);
    }
    // MÉTODOS PARA MEMBRESÍAS
    public Task<IEnumerable<Membresia>> ObtenerMembresiasAsync()
    {
        return _repo.ObtenerMembresiasAsync();
    }

    public Task<IEnumerable<ClienteMembresiaConsulta>> ObtenerClienteMembresiasAsync(int idCliente)
    {
        if (idCliente <= 0) throw new ArgumentException("El ID del cliente es inválido.");
        return _repo.ObtenerClienteMembresiasAsync(idCliente);
    }
    public Task<int> CrearMembresiaAsync(Membresia m)
    {
        if(string.IsNullOrWhiteSpace(m.NombrePlan)||m.Costo<0) throw new ArgumentException("Datos de membresía inválidos.");
        return _repo.CrearMembresiaAsync(m);
    }
    public Task<int> AsignarMembresiaClienteAsync(ClienteMembresia cm)
    {
        if(cm.IdCliente<=0||cm.IdMembresia<=0) throw new ArgumentException("Cliente y membresía son obligatorios.");
        return _repo.AsignarMembresiaClienteAsync(cm);
    }
    //metodo venta desde turno
    public Task<int> CrearVentaDesdeTurnoAsync(
    int idTurno,
    decimal descuento,
    string? observacion)
    {
        if (idTurno <= 0)
            throw new ArgumentException("El ID del turno no es válido.");

        if (descuento < 0)
            throw new ArgumentException("El descuento no puede ser negativo.");

        return _repo.CrearVentaDesdeTurnoAsync(
            idTurno,
            descuento,
            observacion);
    }
    //validacion Categoria
    public Task<IEnumerable<CategoriaProducto>> ObtenerCategoriasProductoAsync() => _repo.ObtenerCategoriasProductoAsync();
    public Task ActualizarCategoriaProductoAsync(int id, string nombre, string? desc) => _repo.ActualizarCategoriaProductoAsync(id, nombre, desc);
    public Task DesactivarCategoriaProductoAsync(int id) => _repo.DesactivarCategoriaProductoAsync(id);

    public Task<IEnumerable<CategoriaServicio>> ObtenerCategoriasServicioAsync() => _repo.ObtenerCategoriasServicioAsync();
    public Task ActualizarCategoriaServicioAsync(int id, string nombre, string? desc) => _repo.ActualizarCategoriaServicioAsync(id, nombre, desc);
    public Task DesactivarCategoriaServicioAsync(int id) => _repo.DesactivarCategoriaServicioAsync(id);
    //validacion de Persona
    private static void ValidarPersona(Persona p)
    {
        if(string.IsNullOrWhiteSpace(p.Nombre)||string.IsNullOrWhiteSpace(p.Apellido))
            throw new ArgumentException("Nombre y apellido son obligatorios.");
        if(p.Nombre.Length>50||p.Apellido.Length>50) throw new ArgumentException("Nombre o apellido demasiado largo.");


    }


    public Task<IEnumerable<ClienteConsulta>> ObtenerClientesAsync()
    {
        return _repo.ObtenerClientesAsync();
    }


    public Task<ClienteConsulta?> ObtenerClientePorIdAsync(int idCliente)
    {
        return _repo.ObtenerClientePorIdAsync(idCliente);
    }

    public Task<IEnumerable<ClienteConsulta>> BuscarClientesAsync(string termino)
    {
        return _repo.BuscarClientesAsync(termino);
    }

    public Task ActualizarClienteAsync(
    int idCliente,
    string? dni,
    string nombre,
    string apellido,
    string? telefono,
    string? email,
    string? direccion)
    {
        return _repo.ActualizarClienteAsync(
            idCliente,
            dni,
            nombre,
            apellido,
            telefono,
            email,
            direccion);
    }

    public Task DesactivarClienteAsync(int idCliente)
    {
        return _repo.DesactivarClienteAsync(idCliente);
    }
    // --- MÉTODOS PARA EL SALÓN ---

    public Task<Salon> ObtenerSalonAsync()
    {
        return _repo.ObtenerSalonAsync();
    }

    public Task ActualizarSalonAsync(int idSalon, string nombre, string direccion, string telefono, string email, string logo)
    {
        // el nombre no es vacio
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del salón es obligatorio.");

        return _repo.ActualizarSalonAsync(idSalon, nombre, direccion, telefono, email, logo);
    }

}


