using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _servicio; 
        public PagosController(IPagoService servicio) => _servicio = servicio;

        [HttpGet("venta/{idVenta}")]
        public async Task<IActionResult> ObtenerPorVenta(int idVenta) => Ok(await _servicio.ObtenerPagosPorVentaAsync(idVenta));

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] PagoRequest request)
        {
            var pago = new Pago { IdVenta = request.IdVenta, Monto = request.Monto, MetodoPago = request.MetodoPago, Referencia = request.Referencia };
            int id = await _servicio.RegistrarPagoAsync(pago);
            return StatusCode(201, new { Mensaje = "Pago registrado.", Id = id });
        }
    }
}