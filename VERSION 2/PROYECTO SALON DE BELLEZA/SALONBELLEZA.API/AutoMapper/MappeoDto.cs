using AutoMapper;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;

namespace SALONBELLEZA.API.AutoMapper
{
    public class MappeoDto : Profile
    {
        public MappeoDto()
        {
            // ==========================================
            // CLIENTES
            // ==========================================
            CreateMap<ClienteConsulta, ClienteDto>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));
            CreateMap<RegistrarClienteRequest, Persona>();
            CreateMap<ClienteActualizarRequest, Persona>();

            // ==========================================
            // EMPLEADOS
            // ==========================================
            CreateMap<EmpleadoConsulta, EmpleadoDto>()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));
            CreateMap<RegistrarEmpleadoRequest, Persona>();
            CreateMap<EmpleadoActualizarRequest, Persona>();

            // ==========================================
            // PROVEEDORES
            // ==========================================
            CreateMap<Proveedor, ProveedorDto>();
            CreateMap<ProveedorRequest, Proveedor>();
            CreateMap<ProveedorActualizarRequest, Proveedor>();

            // ==========================================
            // CATEGORÍAS DE PRODUCTO
            // ==========================================
            CreateMap<CategoriaProducto, CategoriaProductoDto>();
            CreateMap<CategoriaProductoRequest, CategoriaProducto>();
            CreateMap<CategoriaProductoActualizarRequest, CategoriaProducto>();

            // ==========================================
            // PRODUCTOS
            // ==========================================
            CreateMap<ProductoConsulta, ProductoDto>();
            CreateMap<ProductoRequest, Producto>();
            CreateMap<ProductoActualizarRequest, Producto>();

            // ==========================================
            // CATEGORÍAS DE SERVICIO
            // ==========================================
            CreateMap<CategoriaServicio, CategoriaServicioDto>();
            CreateMap<CategoriaServicioRequest, CategoriaServicio>();

            // ==========================================
            // SERVICIOS
            // ==========================================
            CreateMap<ServicioConsulta, ServicioDto>();
            CreateMap<ServicioRequest, Servicio>();
            CreateMap<ServicioActualizarRequest, Servicio>();

            // ==========================================
            // TURNOS
            // ==========================================
            CreateMap<TurnoConsulta, TurnoDto>();
            CreateMap<TurnoRequest, Turno>();

            // ==========================================
            // VENTAS
            // ==========================================
            CreateMap<VentaConsulta, VentaDto>();
            CreateMap<VentaRequest, Venta>();

            // ==========================================
            // PAGOS
            // ==========================================
            CreateMap<PagoConsulta, PagoDto>();
            CreateMap<PagoRequest, Pago>();

            // ==========================================
            // FACTURAS
            // ==========================================
            CreateMap<FacturaConsulta, FacturaDto>();
            CreateMap<FacturaRequest, Factura>();

            // ==========================================
            // MEMBRESÍAS
            // ==========================================
            CreateMap<Membresia, MembresiaDto>();
            CreateMap<MembresiaRequest, Membresia>();
            CreateMap<AsignarMembresiaRequest, ClienteMembresia>();

            // ==========================================
            // SALÓN
            // ==========================================
            CreateMap<Salon, SalonDto>();
            CreateMap<ActualizarSalonRequest, Salon>();

            // ==========================================
            // COMPRAS
            // ==========================================
            CreateMap<CompraRequest, Compra>(); 
        }
    }
}