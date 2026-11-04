using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _servicio; // 🔥 CAMBIO
        private readonly IMapper _mapper;

        public ClientesController(IClienteService servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(_mapper.Map<IEnumerable<ClienteDto>>(await _servicio.ObtenerClientesAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _servicio.ObtenerClientePorIdAsync(id);
            if (cliente == null) return NotFound(new { Mensaje = "Cliente no encontrado" });
            return Ok(_mapper.Map<ClienteDto>(cliente));
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ClienteRequest request)
        {
            try
            {
                var persona = _mapper.Map<Persona>(request);
                int id = await _servicio.RegistrarClienteAsync(persona, request.Categoria);
                return StatusCode(201, new { Mensaje = "Cliente registrado.", Id = id });
            }
            catch (ArgumentException ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteActualizarRequest request)
        {
            if (id != request.IdCliente) return BadRequest(new { Mensaje = "El ID no coincide." });
            try
            {
                await _servicio.ActualizarClienteAsync(id, request.Dni, request.Nombre, request.Apellido, request.Telefono, request.Email, request.Direccion);
                return Ok(new { Mensaje = "Cliente actualizado." });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _servicio.DesactivarClienteAsync(id);
            return NoContent();
        }
    }
}