using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public EmpleadosController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerEmpleadosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var emp = await _servicio.ObtenerEmpleadoPorIdAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarEmpleadoRequest request)
        {
            try
            {
                var persona = new Persona { Dni = request.Dni, Nombre = request.Nombre, Apellido = request.Apellido, Telefono = request.Telefono, Email = request.Email, Direccion = request.Direccion };
                int id = await _servicio.RegistrarEmpleadoAsync(persona, request.TipoEmpleado);
                return StatusCode(201, new { Mensaje = "Empleado registrado.", Id = id });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EmpleadoActualizarRequest request)
        {
            try
            {
                if (id != request.IdEmpleado) return BadRequest("El ID no coincide.");
                await _servicio.ActualizarEmpleadoAsync(id, request.Dni, request.Nombre, request.Apellido, request.Telefono, request.Email, request.Direccion, request.TipoEmpleado);
                return Ok(new { Mensaje = "Empleado actualizado." });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _servicio.DesactivarEmpleadoAsync(id);
            return NoContent();
        }
    }
}