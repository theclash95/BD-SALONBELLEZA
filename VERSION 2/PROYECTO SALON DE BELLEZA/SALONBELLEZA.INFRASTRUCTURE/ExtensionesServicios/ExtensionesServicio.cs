using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.CORE.Servicios;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
using SALONBELLEZA.INFRASTRUCTURE.Repositorios;
namespace SALONBELLEZA.INFRASTRUCTURE.ExtensionesServicios;
public static class ExtensionesServicio
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        var cs=config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No existe ConnectionStrings:DefaultConnection.");
        services.AddSingleton<IMyConexion>(_=>new MyConexion(cs));
        services.AddScoped<ISalonRepositorio,SalonRepositorio>();
        services.AddScoped<ServicioSalon>();
        return services;
    }
}