using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoService _servicio; // 🔥 CAMBIO
        public TurnosController(ITurnoService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerTurnosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var t = await _servicio.ObtenerTurnoPorIdAsync(id);
            return t == null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TurnoRequest request)
        {
            var turno = new Turno { IdCliente = request.IdCliente, IdEmpleado = request.IdEmpleado, Fecha = request.Fecha, Hora = request.Hora, Observacion = request.Observacion };
            int id = await _servicio.CrearTurnoAsync(turno);
            return StatusCode(201, new { Mensaje = "Turno creado.", Id = id });
        }

        [HttpPost("{id}/servicios")]
        public async Task<IActionResult> AgregarServicio(int id, [FromBody] DetalleTurnoRequest request)
        {
            await _servicio.AgregarServicioATurnoAsync(id, request.IdServicio, request.Descuento);
            return Ok(new { Mensaje = "Servicio agregado al turno." });
        }
    }
}