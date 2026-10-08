using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;
namespace SALONBELLEZA.CORE.Servicios;
public class ServicioSalon
{
    private readonly ISalonRepositorio _repo;
    public ServicioSalon(ISalonRepositorio repo) => _repo = repo;

    public Task<int> RegistrarClienteAsync(Persona p,string? categoria)
    {
        ValidarPersona(p);
        return _repo.RegistrarClienteAsync(p,categoria);
    }
    public Task<int> RegistrarEmpleadoAsync(Persona p,string? tipo)
    {
        ValidarPersona(p);
        return _repo.RegistrarEmpleadoAsync(p,tipo);
    }
    public Task<int> RegistrarCategoriaProductoAsync(string n,string? d)
    {
        if(string.IsNullOrWhiteSpace(n)) throw new ArgumentException("El nombre es obligatorio.");
        return _repo.RegistrarCategoriaProductoAsync(n,d);
    }

    public Task<int> RegistrarCategoriaServicioAsync(string nombre, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la categoría de servicio es obligatorio.");

        return _repo.RegistrarCategoriaServicioAsync(nombre, descripcion);
    }

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
    public Task<int> RegistrarProveedorAsync(Proveedor p)
    {
        if(string.IsNullOrWhiteSpace(p.RazonSocial)) throw new ArgumentException("La razón social es obligatoria.");
        return _repo.RegistrarProveedorAsync(p);
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
    public Task<int> RealizarVentaProductoAsync(int idCliente,int idProducto,int cantidad)
    {
        if(idCliente<=0||idProducto<=0||cantidad<=0) throw new ArgumentException("Datos de venta inválidos.");
        return _repo.RealizarVentaProductoAsync(idCliente,idProducto,cantidad);
    }
    public Task<int> RegistrarPagoAsync(Pago p)
    {
        if(p.IdVenta<=0||p.Monto is null||p.Monto<0) throw new ArgumentException("Datos de pago inválidos.");
        return _repo.RegistrarPagoAsync(p);
    }
    public Task<int> GenerarFacturaAsync(Factura f)
    {
        if(f.IdVenta<=0||string.IsNullOrWhiteSpace(f.NumeroFactura)||string.IsNullOrWhiteSpace(f.TipoComprobante))
            throw new ArgumentException("Datos de factura inválidos.");
        return _repo.GenerarFacturaAsync(f);
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


    private static void ValidarPersona(Persona p)
    {
        if(string.IsNullOrWhiteSpace(p.Nombre)||string.IsNullOrWhiteSpace(p.Apellido))
            throw new ArgumentException("Nombre y apellido son obligatorios.");
        if(p.Nombre.Length>50||p.Apellido.Length>50) throw new ArgumentException("Nombre o apellido demasiado largo.");
    }
}