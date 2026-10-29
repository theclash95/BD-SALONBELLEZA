using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public FacturasController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var facturas = await _servicio.ObtenerFacturasAsync();
                return Ok(facturas);
            }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var factura = await _servicio.ObtenerFacturaPorIdAsync(id);
                if (factura == null) return NotFound(new { Mensaje = "Factura no encontrada." });
                return Ok(factura);
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Generar([FromBody] FacturaRequest request)
        {
            try
            {
                var factura = new Factura
                {
                    IdVenta = request.IdVenta,
                    NumeroFactura = request.NumeroFactura,
                    TipoComprobante = request.TipoComprobante
                };
                int id = await _servicio.GenerarFacturaAsync(factura);
                return StatusCode(201, new { Mensaje = "Factura generada.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }
    }
}