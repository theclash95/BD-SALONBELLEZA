using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _servicio; // 🔥 CAMBIO
        public ComprasController(ICompraService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas() => Ok(await _servicio.ObtenerComprasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var c = await _servicio.ObtenerCompraPorIdAsync(id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] CompraRequest request)
        {
            int id = await _servicio.RegistrarCompraAsync(request.IdProveedor, request.IdProducto, request.Cantidad, request.PrecioCosto, request.Observacion);
            return StatusCode(201, new { Mensaje = "Compra registrada.", Id = id });
        }
    }
}