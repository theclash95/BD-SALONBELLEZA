using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _servicio; // 🔥 CAMBIO
        public VentasController(IVentaService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas() => Ok(await _servicio.ObtenerVentasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var v = await _servicio.ObtenerVentaPorIdAsync(id);
            return v == null ? NotFound() : Ok(v);
        }

        [HttpPost("turno")]
        public async Task<IActionResult> CrearDesdeTurno([FromBody] VentaRequest request)
        {
            int id = await _servicio.CrearVentaDesdeTurnoAsync(request.IdTurno, request.Descuento, request.Observacion);
            return Ok(new { Mensaje = "Venta generada.", Id = id });
        }

        [HttpPost("producto")]
        public async Task<IActionResult> RealizarVentaProducto([FromBody] VentaProductoRequest request)
        {
            int id = await _servicio.RealizarVentaProductoAsync(request.IdCliente, request.IdProducto, request.Cantidad);
            return Ok(new { Mensaje = "Venta de producto realizada.", Id = id });
        }
    }
}