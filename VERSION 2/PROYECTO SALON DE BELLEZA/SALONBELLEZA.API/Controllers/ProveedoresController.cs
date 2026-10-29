using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public ProveedoresController(ServicioSalon servicio) => _servicio = servicio;

        // 1. GET: api/Proveedores (Listar todos)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var proveedores = await _servicio.ObtenerProveedoresAsync();
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error al obtener proveedores", Detalle = ex.Message });
            }
        }

        // 2. GET: api/Proveedores/5 (Obtener por ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var proveedor = await _servicio.ObtenerProveedorPorIdAsync(id);
                if (proveedor == null)
                    return NotFound(new { Mensaje = "Proveedor no encontrado." });

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error al obtener el proveedor", Detalle = ex.Message });
            }
        }

        // 3. POST: api/Proveedores (Registrar)
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ProveedorRequest request)
        {
            try
            {
                var proveedor = new Proveedor
                {
                    RazonSocial = request.RazonSocial,
                    Cuit = request.Cuit,
                    Telefono = request.Telefono,
                    Email = request.Email,
                    Direccion = request.Direccion
                };
                int id = await _servicio.RegistrarProveedorAsync(proveedor);
                return StatusCode(201, new { Mensaje = "Proveedor registrado exitosamente.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        // 4. PUT: api/Proveedores/5 (Actualizar)
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProveedorRequest request)
        {
            try
            {
                await _servicio.ActualizarProveedorAsync(id, request.RazonSocial, request.Cuit, request.Telefono, request.Email, request.Direccion);
                return Ok(new { Mensaje = "Proveedor actualizado correctamente." });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        // 5. DELETE: api/Proveedores/5 (Desactivar - Baja Lógica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                await _servicio.DesactivarProveedorAsync(id);
                return NoContent(); // 204 No Content (Éxito sin cuerpo de respuesta)
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error al desactivar el proveedor", Detalle = ex.Message });
            }
        }
    }
}