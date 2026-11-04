using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SALONBELLEZA.API.AutoMapper;
using SALONBELLEZA.API.Middlewares;
using SALONBELLEZA.INFRASTRUCTURE.ExtensionesServicios;
using Microsoft.OpenApi;

namespace SALONBELLEZA.API;

public class Program
{
    public static void Main(string[] args)
    {
        //Configuración global de Dapper (Mapea id_cliente a IdCliente automáticamente)
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var builder = WebApplication.CreateBuilder(args);

        // ==========================================
        // 1. INYECCIÓN DE INFRAESTRUCTURA (Conexión, Repos y Servicios)
        // ==========================================
        builder.Services.AddInfrastructure(builder.Configuration);

        // ==========================================
        // 2. AUTOMAPPER (Sintaxis correcta para v16)
        // ==========================================
        builder.Services.AddAutoMapper(cfg => { }, typeof(MappeoDto));

        // ==========================================
        // 3. VALIDADORES (FluentValidation)
        // ==========================================
        // Escanea todo el proyecto y registra todos los validadores automáticamente.
        // (Requiere el paquete FluentValidation.DependencyInjectionExtensions que ya instalaste)
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        // NOTA: Si el escaneo automático te diera algún problema, 
        // podés borrar la línea de arriba y descomentar el registro manual:
        /*
        builder.Services.AddScoped<IValidator<ClienteRequest>, ClienteCrearValidator>();
        builder.Services.AddScoped<IValidator<ClienteActualizarRequest>, ClienteActualizarValidator>();
        builder.Services.AddScoped<IValidator<EmpleadoRequest>, EmpleadoCrearValidator>();
        builder.Services.AddScoped<IValidator<EmpleadoActualizarRequest>, EmpleadoActualizarValidator>();
        builder.Services.AddScoped<IValidator<ProveedorRequest>, ProveedorCrearValidator>();
        builder.Services.AddScoped<IValidator<ProveedorActualizarRequest>, ProveedorActualizarValidator>();
        builder.Services.AddScoped<IValidator<ProductoRequest>, ProductoCrearValidator>();
        builder.Services.AddScoped<IValidator<ProductoActualizarRequest>, ProductoActualizarValidator>();
        builder.Services.AddScoped<IValidator<CategoriaProductoRequest>, CategoriaProductoValidator>();
        builder.Services.AddScoped<IValidator<CategoriaProductoActualizarRequest>, CategoriaProductoActualizarValidator>();
        builder.Services.AddScoped<IValidator<CategoriaServicioRequest>, CategoriaServicioValidator>();
        builder.Services.AddScoped<IValidator<CategoriaServicioActualizarRequest>, CategoriaServicioActualizarValidator>();
        builder.Services.AddScoped<IValidator<ServicioRequest>, ServicioCrearValidator>();
        builder.Services.AddScoped<IValidator<ServicioActualizarRequest>, ServicioActualizarValidator>();
        builder.Services.AddScoped<IValidator<TurnoRequest>, TurnoCrearValidator>();
        builder.Services.AddScoped<IValidator<PagoRequest>, PagoCrearValidator>();
        builder.Services.AddScoped<IValidator<FacturaRequest>, FacturaCrearValidator>();
        builder.Services.AddScoped<IValidator<MembresiaRequest>, MembresiaCrearValidator>();
        builder.Services.AddScoped<IValidator<AsignarMembresiaRequest>, AsignarMembresiaValidator>();
        builder.Services.AddScoped<IValidator<CompraRequest>, CompraCrearValidator>();
        builder.Services.AddScoped<IValidator<StockRequest>, StockValidator>();
        builder.Services.AddScoped<IValidator<ActualizarSalonRequest>, SalonActualizarValidator>();
        */

        // ==========================================
        // 4. CORS (Para conectar con el Frontend)
        // ==========================================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5173",
            "http://localhost:4200",
            "http://localhost:5500",
            "http://localhost:5180",
            "http://localhost:7180",
            "http://127.0.0.1:5500"
                      )
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // ==========================================
        // 5. CONTROLADORES Y SWAGGER
        // ==========================================
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SALONBELLEZA API",
                Version = "v1",
                Description = "API para la gestión del salón de belleza"
            });
        });
        var app = builder.Build();

        // ==========================================
        // 6. MIDDLEWARE GLOBAL DE ERRORES
        // ==========================================
        // Atrapa todas las excepciones y devuelve respuestas JSON limpias
        app.UseMiddleware<ManejadorErroresMiddleware>();

        // ==========================================
        // 7. PIPELINE HTTP
        // ==========================================
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // CORS debe ir antes de Authorization y MapControllers
        app.UseCors("AllowFrontend");

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}