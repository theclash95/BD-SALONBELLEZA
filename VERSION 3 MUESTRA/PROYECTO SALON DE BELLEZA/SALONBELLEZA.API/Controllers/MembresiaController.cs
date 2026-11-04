using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiaService _servicio; // 🔥 CAMBIO
        public MembresiasController(IMembresiaService servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas() => Ok(await _servicio.ObtenerMembresiasAsync());

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MembresiaRequest request)
        {
            var membresia = new Membresia { NombrePlan = request.NombrePlan, Descripcion = request.Descripcion, Costo = request.Costo, DuracionDias = request.DuracionDias, PorcentajeDescuento = request.PorcentajeDescuento };
            int id = await _servicio.CrearMembresiaAsync(membresia);
            return StatusCode(201, new { Mensaje = "Membresía creada.", Id = id });
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarACliente([FromBody] AsignarMembresiaRequest request)
        {
            var cm = new ClienteMembresia { IdCliente = request.IdCliente, IdMembresia = request.IdMembresia, Observacion = request.Observacion };
            int id = await _servicio.AsignarMembresiaClienteAsync(cm);
            return StatusCode(201, new { Mensaje = "Membresía asignada.", Id = id });
        }
    }
}