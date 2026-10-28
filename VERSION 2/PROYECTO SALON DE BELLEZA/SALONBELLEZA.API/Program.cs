using FluentValidation;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.API.Validadores;
using SALONBELLEZA.INFRASTRUCTURE.ExtensionesServicios;

namespace SALONBELLEZA.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IValidator<RegistrarClienteRequest>, ClienteCrearValidator>();

        /* CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA */
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("WebSalon", policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:5500",
                        "http://127.0.0.1:5500"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });






        var app = builder.Build();

        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.UseHttpsRedirection();

        /* CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA CODIGO DE WEB PRUEBA */
        app.UseCors("WebSalon");




        app.MapControllers();
        app.Run();
    }
}   