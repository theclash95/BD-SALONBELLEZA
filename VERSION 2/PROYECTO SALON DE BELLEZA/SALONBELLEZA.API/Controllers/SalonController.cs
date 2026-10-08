using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers;

[ApiController]
[Route("api/salon")]
public class SalonController : ControllerBase
{
    private readonly ServicioSalon _servicio;
    public SalonController(ServicioSalon servicio) => _servicio=servicio;

    private async Task<IActionResult> Ejecutar(Func<Task<int>> accion)
    {
        try
        {
            var id=await accion();
            return Ok(new { mensaje="Operación realizada correctamente.", id });
        }
        catch(ArgumentException ex){ return BadRequest(new { mensaje=ex.Message }); }
        catch(Exception ex){ return StatusCode(500,new { mensaje="Error al ejecutar la operación.", detalle=ex.Message }); }
    }

    [HttpPost("clientes")]
    public Task<IActionResult> RegistrarCliente(RegistrarClienteRequest r) =>
        Ejecutar(()=>_servicio.RegistrarClienteAsync(new Persona{Dni=r.Dni,Nombre=r.Nombre,Apellido=r.Apellido,Telefono=r.Telefono,Email=r.Email,Direccion=r.Direccion},r.Categoria));

    [HttpPost("empleados")]
    public Task<IActionResult> RegistrarEmpleado(RegistrarEmpleadoRequest r) =>
        Ejecutar(()=>_servicio.RegistrarEmpleadoAsync(new Persona{Dni=r.Dni,Nombre=r.Nombre,Apellido=r.Apellido,Telefono=r.Telefono,Email=r.Email,Direccion=r.Direccion},r.TipoEmpleado));

    [HttpPost("categorias-producto")]
    public Task<IActionResult> CategoriaProducto(CategoriaProductoRequest r) =>
        Ejecutar(()=>_servicio.RegistrarCategoriaProductoAsync(r.Nombre,r.Descripcion));

    [HttpPost("productos")]
    public Task<IActionResult> Producto(ProductoRequest r) =>
        Ejecutar(()=>_servicio.RegistrarProductoAsync(new Producto{IdCategoriaProducto=r.IdCategoriaProducto,Codigo=r.Codigo,Nombre=r.Nombre,Descripcion=r.Descripcion,PrecioCosto=r.PrecioCosto,PrecioVenta=r.PrecioVenta,StockActual=r.Stock,StockMinimo=r.StockMinimo}));

    [HttpPost("productos/stock")]
    public Task<IActionResult> Stock(StockRequest r) =>
        Ejecutar(()=>_servicio.ActualizarStockProductoAsync(r.IdProducto,r.Cantidad,r.TipoMovimiento,r.Motivo));

    [HttpPost("proveedores")]
    public Task<IActionResult> Proveedor(ProveedorRequest r) =>
        Ejecutar(()=>_servicio.RegistrarProveedorAsync(new Proveedor{RazonSocial=r.RazonSocial,Cuit=r.Cuit,Telefono=r.Telefono,Email=r.Email,Direccion=r.Direccion}));

    [HttpPost("compras")]
    public Task<IActionResult> Compra(CompraRequest r) =>
        Ejecutar(()=>_servicio.RegistrarCompraAsync(r.IdProveedor,r.IdProducto,r.Cantidad,r.PrecioCosto,r.Observacion));

    [HttpPost("categorias-servicio")]
    public Task<IActionResult> RegistrarCategoriaServicio(CategoriaServicioRequest r) =>
    Ejecutar(() => _servicio.RegistrarCategoriaServicioAsync(
        r.Nombre,
        r.Descripcion
    ));

    [HttpPost("servicios")]
    public Task<IActionResult> Servicio(ServicioRequest r) =>
        Ejecutar(()=>_servicio.RegistrarServicioAsync(new Servicio{IdCategoriaServicio=r.IdCategoria,Nombre=r.Nombre,Descripcion=r.Descripcion,Precio=r.Precio,DuracionMinuto=r.Duracion}));

    [HttpPost("turnos")]
    public Task<IActionResult> Turno(TurnoRequest r) =>
        Ejecutar(()=>_servicio.CrearTurnoAsync(new Turno{IdCliente=r.IdCliente,IdEmpleado=r.IdEmpleado,Fecha=r.Fecha,Hora=r.Hora,Observacion=r.Observacion}));

    [HttpPost("ventas")]
    public Task<IActionResult> CrearVenta(VentaRequest r) =>
    Ejecutar(() => _servicio.CrearVentaDesdeTurnoAsync(
        r.IdTurno,
        r.Descuento,
        r.Observacion
    ));


    [HttpPost("turnos/servicios")]
    public Task<IActionResult> DetalleTurno(DetalleTurnoRequest r) =>
        Ejecutar(()=>_servicio.AgregarServicioATurnoAsync(r.IdTurno,r.IdServicio,r.Descuento));

    [HttpPost("ventas/productos")]
    public Task<IActionResult> VentaProducto(VentaProductoRequest r) =>
        Ejecutar(()=>_servicio.RealizarVentaProductoAsync(r.IdCliente,r.IdProducto,r.Cantidad));


    [HttpPost("pagos")]
    public Task<IActionResult> Pago(PagoRequest r) =>
        Ejecutar(()=>_servicio.RegistrarPagoAsync(new Pago{IdVenta=r.IdVenta,Monto=r.Monto,MetodoPago=r.MetodoPago,Referencia=r.Referencia}));

    [HttpPost("facturas")]
    public Task<IActionResult> Factura(FacturaRequest r) =>
        Ejecutar(()=>_servicio.GenerarFacturaAsync(new Factura{IdVenta=r.IdVenta,NumeroFactura=r.NumeroFactura,TipoComprobante=r.TipoComprobante}));

    [HttpPost("membresias")]
    public Task<IActionResult> Membresia(MembresiaRequest r) =>
        Ejecutar(()=>_servicio.CrearMembresiaAsync(new Membresia{NombrePlan=r.NombrePlan,Descripcion=r.Descripcion,Costo=r.Costo,DuracionDias=r.DuracionDias,PorcentajeDescuento=r.PorcentajeDescuento}));

    [HttpPost("membresias/asignar")]
    public Task<IActionResult> AsignarMembresia(AsignarMembresiaRequest r) =>
        Ejecutar(()=>_servicio.AsignarMembresiaClienteAsync(new ClienteMembresia{IdCliente=r.IdCliente,IdMembresia=r.IdMembresia,Observacion=r.Observacion}));

    [HttpGet("estado")]
    public IActionResult Estado() => Ok(new { sistema="SALONBELLEZA", estado="API funcionando", fecha=DateTime.Now });
}
