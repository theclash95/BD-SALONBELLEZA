using System;

namespace SALONBELLEZA.CORE.Entidades
{
    public class EmpleadoConsulta
    {
        public int IdEmpleado { get; set; }
        public int IdPersona { get; set; }
        public string? Dni { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? TipoEmpleado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Estado { get; set; }
    }

    public class ProductoConsulta
    {
        public int IdProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public string? Codigo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal? PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int? StockActual { get; set; }
        public int? StockMinimo { get; set; }
        public string? Estado { get; set; }
    }

    public class MovimientoStockConsulta
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string? TipoMovimiento { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? Motivo { get; set; }
        public int? StockAnterior { get; set; }
        public int? StockPosterior { get; set; }
    }

    public class CompraConsulta
    {
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public DateTime? FechaHora { get; set; }
        public string? Estado { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
        public string? Observacion { get; set; }
    }

    public class ServicioConsulta
    {
        public int IdServicio { get; set; }
        public int IdCategoriaServicio { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? DuracionMinuto { get; set; }
        public string? Estado { get; set; }
    }

    public class TurnoConsulta
    {
        public int IdTurno { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string? Estado { get; set; }
        public string? Observacion { get; set; }
    }

    public class VentaConsulta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime? FechaHora { get; set; }
        public string? TipoVenta { get; set; }
        public string? Estado { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public string? Observacion { get; set; }
    }

    public class PagoConsulta
    {
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? Monto { get; set; }
        public string? MetodoPago { get; set; }
        public string? Estado { get; set; }
        public string? Referencia { get; set; }
    }

    public class FacturaConsulta
    {
        public int IdFactura { get; set; }
        public int IdVenta { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string? TipoComprobante { get; set; }
        public string? Estado { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Total { get; set; }
        public string Cliente { get; set; } = string.Empty;
    }

    public class ClienteMembresiaConsulta
    {
        public int IdClienteMembresia { get; set; }
        public string NombrePlan { get; set; } = string.Empty;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Estado { get; set; }
        public string? Observacion { get; set; }
    }
}