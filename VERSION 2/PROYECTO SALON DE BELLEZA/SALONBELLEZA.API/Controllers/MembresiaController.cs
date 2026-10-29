using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembresiasController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public MembresiasController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var membresias = await _servicio.ObtenerMembresiasAsync();
                return Ok(membresias);
            }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MembresiaRequest request)
        {
            try
            {
                var membresia = new Membresia
                {
                    NombrePlan = request.NombrePlan,
                    Descripcion = request.Descripcion,
                    Costo = request.Costo,
                    DuracionDias = request.DuracionDias,
                    PorcentajeDescuento = request.PorcentajeDescuento
                };
                int id = await _servicio.CrearMembresiaAsync(membresia);
                return StatusCode(201, new { Mensaje = "Membresía creada.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarACliente([FromBody] AsignarMembresiaRequest request)
        {
            try
            {
                var cm = new ClienteMembresia
                {
                    IdCliente = request.IdCliente,
                    IdMembresia = request.IdMembresia,
                    Observacion = request.Observacion
                };
                int id = await _servicio.AsignarMembresiaClienteAsync(cm);
                return StatusCode(201, new { Mensaje = "Membresía asignada.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ObtenerDeCliente(int idCliente)
        {
            try
            {
                var membresias = await _servicio.ObtenerClienteMembresiasAsync(idCliente);
                return Ok(membresias);
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); }
        }
    }
}