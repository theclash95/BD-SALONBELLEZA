using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalonController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        private readonly IMapper _mapper;

        public SalonController(ServicioSalon servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }

        // 1. GET: api/salon (Devuelve la info del salón)
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var salon = await _servicio.ObtenerSalonAsync();
                var salonDto = _mapper.Map<SalonDto>(salon);
                return Ok(salonDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message });
            }
        }

        // 2. PUT: api/salon (Actualiza la info del salón)
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarSalonRequest request)
        {
            try
            {
                // Necesitamos el ID del salón. Asumimos que es el 1 porque es un solo local.
                // Si tuvieras varios, lo recibirías por URL.
                int idSalon = 1;

                await _servicio.ActualizarSalonAsync(
                    idSalon,
                    request.Nombre ?? string.Empty,
                    request.Direccion ?? string.Empty,
                    request.Telefono ?? string.Empty,
                    request.Email ?? string.Empty,
                    request.Logo ?? string.Empty
                );

                return Ok(new { Mensaje = "Datos del salón actualizados correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message });
            }
        }

        // 3. GET: api/salon/estado (Health check, para ver si la API responde)
        [HttpGet("estado")]
        public IActionResult Estado() => Ok(new { sistema = "SALONBELLEZA", estado = "API funcionando", fecha = DateTime.Now });
    }
}
