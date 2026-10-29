using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public PagosController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet("venta/{idVenta}")]
        public async Task<IActionResult> ObtenerPorVenta(int idVenta)
        {
            try
            {
                var pagos = await _servicio.ObtenerPagosPorVentaAsync(idVenta);
                return Ok(pagos);
            }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] PagoRequest request)
        {
            try
            {
                var pago = new Pago
                {
                    IdVenta = request.IdVenta,
                    Monto = request.Monto,
                    MetodoPago = request.MetodoPago,
                    Referencia = request.Referencia
                };
                int id = await _servicio.RegistrarPagoAsync(pago);
                return StatusCode(201, new { Mensaje = "Pago registrado.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }
    }
}