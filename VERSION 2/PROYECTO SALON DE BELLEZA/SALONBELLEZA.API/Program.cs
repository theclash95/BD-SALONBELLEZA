using FluentValidation;
using SALONBELLEZA.API.AutoMapper;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.API.Validadores;
using SALONBELLEZA.CORE.Interfaces;
using SALONBELLEZA.CORE.Servicios;
using SALONBELLEZA.INFRASTRUCTURE.Contexto;
using SALONBELLEZA.INFRASTRUCTURE.Repositorios;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
var builder = WebApplication.CreateBuilder(args);

// 1. Inyección de Base de Datos y Repositorios
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IMyConexion>(new MyConexion(connectionString!));
builder.Services.AddScoped<ISalonRepositorio, SalonRepositorio>();

// 2. Servicios (Lógica de Negocio)
builder.Services.AddScoped<ServicioSalon>();

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(MappeoDto));

// 4. Validadores
builder.Services.AddScoped<IValidator<RegistrarClienteRequest>, ClienteCrearValidator>();

// 5. Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6. Configuración de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();