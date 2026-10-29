using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public ComprasController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var compras = await _servicio.ObtenerComprasAsync();
                return Ok(compras);
            }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var compra = await _servicio.ObtenerCompraPorIdAsync(id);
                if (compra == null) return NotFound(new { Mensaje = "Compra no encontrada." });
                return Ok(compra);
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] CompraRequest request)
        {
            try
            {
                int id = await _servicio.RegistrarCompraAsync(request.IdProveedor, request.IdProducto, request.Cantidad, request.PrecioCosto, request.Observacion);
                return StatusCode(201, new { Mensaje = "Compra registrada y stock actualizado.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }
    }
}