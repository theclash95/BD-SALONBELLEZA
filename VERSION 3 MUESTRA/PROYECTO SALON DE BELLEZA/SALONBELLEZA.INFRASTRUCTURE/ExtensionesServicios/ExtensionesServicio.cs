using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.CORE.Servicios;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
using SALONBELLEZA.INFRASTRUCTURE.Repositorios;

namespace SALONBELLEZA.INFRASTRUCTURE.ExtensionesServicios;

public static class ExtensionesServicio
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No existe ConnectionStrings:DefaultConnection.");

        // Conexión
        services.AddSingleton<IMyConexion>(_ => new MyConexion(cs));

        // Repositorios
        services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
        services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
        services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
        services.AddScoped<ICategoriaProductoRepositorio, CategoriaProductoRepositorio>();
        services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
        services.AddScoped<ICompraRepositorio, CompraRepositorio>();
        services.AddScoped<ICategoriaServicioRepositorio, CategoriaServicioRepositorio>();
        services.AddScoped<IServicioRepositorio, ServicioRepositorio>();
        services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
        services.AddScoped<IVentaRepositorio, VentaRepositorio>();
        services.AddScoped<IPagoRepositorio, PagoRepositorio>();
        services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();
        services.AddScoped<IMembresiaRepositorio, MembresiaRepositorio>();
        services.AddScoped<ISalonRepositorio, SalonRepositorio>();

        // Servicios (Core)
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IEmpleadoService, EmpleadoService>();
        services.AddScoped<IProveedorService, ProveedorService>();
        services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<ICompraService, CompraService>();
        services.AddScoped<ICategoriaServicioService, CategoriaServicioService>();
        services.AddScoped<IServicioService, ServicioService>();
        services.AddScoped<ITurnoService, TurnoService>();
        services.AddScoped<IVentaService, VentaService>();
        services.AddScoped<IPagoService, PagoService>();
        services.AddScoped<IFacturaService, FacturaService>();
        services.AddScoped<IMembresiaService, MembresiaService>();
        services.AddScoped<ISalonService, SalonService>();

        return services;
    }
}