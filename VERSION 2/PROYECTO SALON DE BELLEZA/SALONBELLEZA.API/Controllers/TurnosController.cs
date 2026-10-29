using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public TurnosController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerTurnosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var turno = await _servicio.ObtenerTurnoPorIdAsync(id);
            return turno == null ? NotFound() : Ok(turno);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TurnoRequest request)
        {
            try
            {
                var turno = new Turno { IdCliente = request.IdCliente, IdEmpleado = request.IdEmpleado, Fecha = request.Fecha, Hora = request.Hora, Observacion = request.Observacion };
                int id = await _servicio.CrearTurnoAsync(turno);
                return StatusCode(201, new { Mensaje = "Turno creado.", Id = id });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string estado)
        {
            await _servicio.CambiarEstadoTurnoAsync(id, estado);
            return Ok(new { Mensaje = "Estado del turno actualizado." });
        }
    }
}