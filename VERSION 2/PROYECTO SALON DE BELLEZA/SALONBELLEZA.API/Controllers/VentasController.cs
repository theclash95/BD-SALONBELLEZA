using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public VentasController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var ventas = await _servicio.ObtenerVentasAsync();
                return Ok(ventas);
            }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var venta = await _servicio.ObtenerVentaPorIdAsync(id);
                if (venta == null) return NotFound(new { Mensaje = "Venta no encontrada." });
                return Ok(venta);
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost("turno")]
        public async Task<IActionResult> CrearDesdeTurno([FromBody] VentaRequest request)
        {
            try
            {
                int id = await _servicio.CrearVentaDesdeTurnoAsync(request.IdTurno, request.Descuento, request.Observacion);
                return Ok(new { Mensaje = "Venta generada desde turno.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost("producto")]
        public async Task<IActionResult> RealizarVentaProducto([FromBody] VentaProductoRequest request)
        {
            try
            {
                int id = await _servicio.RealizarVentaProductoAsync(request.IdCliente, request.IdProducto, request.Cantidad);
                return Ok(new { Mensaje = "Venta de producto realizada.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }
    }
}