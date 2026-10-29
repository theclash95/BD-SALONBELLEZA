using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        private readonly IMapper _mapper;

        // Inyectamos el Servicio y AutoMapper (MappeoDto)
        public ClientesController(ServicioSalon servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }

        // 1. GET: api/clientes (Devuelve 200 OK con la lista)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientesBd = await _servicio.ObtenerClientesAsync();

            // Usamos AutoMapper para convertir la entidad al DTO
            var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientesBd);

            return Ok(clientesDto);
        }

        // 2. GET: api/clientes/5 (Devuelve 200 OK o 404 Not Found)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var cliente = await _servicio.ObtenerClientePorIdAsync(id);
                var clienteDto = _mapper.Map<ClienteDto>(cliente);
                return Ok(clienteDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensaje = ex.Message }); // 404
            }
        }

        // 3. POST: api/clientes (Devuelve 201 Created o 400 Bad Request)
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistrarClienteRequest request)
        {
            try
            {
                // AutoMapper pasa los datos del Request a la Entidad Persona
                var persona = _mapper.Map<Persona>(request);

                // Llamamos al servicio pasando la categoría
                int id = await _servicio.RegistrarClienteAsync(persona, request.Categoria);

                return StatusCode(201, new { Mensaje = "Cliente registrado exitosamente.", Id = id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensaje = ex.Message }); // 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message }); // 500
            }
        }

        // 4. PUT: api/clientes/5 (Devuelve 200 OK o 400 Bad Request)
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteActualizarRequest request)
        {
            if (id != request.IdCliente)
                return BadRequest(new { Mensaje = "El ID de la URL no coincide con el ID del cuerpo." });

            try
            {
                await _servicio.ActualizarClienteAsync(id, request.Dni, request.Nombre, request.Apellido, request.Telefono, request.Email, request.Direccion);
                return Ok(new { Mensaje = "Cliente actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message });
            }
        }

        // 5. DELETE: api/clientes/5 (Devuelve 204 No Content)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                await _servicio.DesactivarClienteAsync(id);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = "Error interno", Detalle = ex.Message });
            }
        }
    }
}