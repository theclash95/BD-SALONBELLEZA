using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _servicio; // 🔥 CAMBIO
        public FacturasController(IFacturaService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas() => Ok(await _servicio.ObtenerFacturasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var f = await _servicio.ObtenerFacturaPorIdAsync(id);
            return f == null ? NotFound() : Ok(f);
        }

        [HttpPost]
        public async Task<IActionResult> Generar([FromBody] FacturaRequest request)
        {
            var factura = new Factura { IdVenta = request.IdVenta, NumeroFactura = request.NumeroFactura, TipoComprobante = request.TipoComprobante };
            int id = await _servicio.GenerarFacturaAsync(factura);
            return StatusCode(201, new { Mensaje = "Factura generada.", Id = id });
        }
    }
}