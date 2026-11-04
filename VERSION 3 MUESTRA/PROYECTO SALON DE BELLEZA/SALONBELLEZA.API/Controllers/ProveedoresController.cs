using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _servicio; // 🔥 CAMBIO

        public ProveedoresController(IProveedorService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerProveedoresAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var p = await _servicio.ObtenerProveedorPorIdAsync(id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ProveedorRequest request)
        {
            try
            {
                var proveedor = new Proveedor { RazonSocial = request.RazonSocial, Cuit = request.Cuit, Telefono = request.Telefono, Email = request.Email, Direccion = request.Direccion };
                int id = await _servicio.RegistrarProveedorAsync(proveedor);
                return StatusCode(201, new { Mensaje = "Proveedor registrado.", Id = id });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProveedorActualizarRequest request)
        {
            await _servicio.ActualizarProveedorAsync(id, request.RazonSocial, request.Cuit, request.Telefono, request.Email, request.Direccion);
            return Ok(new { Mensaje = "Proveedor actualizado." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _servicio.DesactivarProveedorAsync(id);
            return NoContent();
        }
    }
}