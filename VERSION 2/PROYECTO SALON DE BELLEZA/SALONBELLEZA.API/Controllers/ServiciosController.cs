using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public ServiciosController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerServiciosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var serv = await _servicio.ObtenerServicioPorIdAsync(id);
            return serv == null ? NotFound() : Ok(serv);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ServicioRequest request)
        {
            try
            {
                var servicio = new Servicio { IdCategoriaServicio = request.IdCategoria, Nombre = request.Nombre, Descripcion = request.Descripcion, Precio = request.Precio, DuracionMinuto = request.Duracion };
                int id = await _servicio.RegistrarServicioAsync(servicio);
                return StatusCode(201, new { Mensaje = "Servicio registrado.", Id = id });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ServicioActualizarRequest request)
        {
            try
            {
                await _servicio.ActualizarServicioAsync(id, request.IdCategoriaServicio, request.Nombre, request.Descripcion, request.Precio, request.DuracionMinuto);
                return Ok(new { Mensaje = "Servicio actualizado." });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _servicio.DesactivarServicioAsync(id);
            return NoContent();
        }
    }
}