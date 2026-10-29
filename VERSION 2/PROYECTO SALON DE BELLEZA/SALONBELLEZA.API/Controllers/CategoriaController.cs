using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public CategoriasController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet("producto")]
        public async Task<IActionResult> ObtenerCatProductos() => Ok(await _servicio.ObtenerCategoriasProductoAsync());

        [HttpGet("servicio")]
        public async Task<IActionResult> ObtenerCatServicios() => Ok(await _servicio.ObtenerCategoriasServicioAsync());

        [HttpPost("producto")]
        public async Task<IActionResult> CrearCatProducto([FromBody] CategoriaProductoRequest request)
        {
            int id = await _servicio.RegistrarCategoriaProductoAsync(request.Nombre, request.Descripcion);
            return StatusCode(201, new { Mensaje = "Categoría de producto creada.", Id = id });
        }

        [HttpPost("servicio")]
        public async Task<IActionResult> CrearCatServicio([FromBody] CategoriaServicioRequest request)
        {
            int id = await _servicio.RegistrarCategoriaServicioAsync(request.Nombre, request.Descripcion);
            return StatusCode(201, new { Mensaje = "Categoría de servicio creada.", Id = id });
        }

        [HttpDelete("producto/{id}")]
        public async Task<IActionResult> DesactivarCatProducto(int id)
        {
            await _servicio.DesactivarCategoriaProductoAsync(id);
            return NoContent();
        }

        [HttpDelete("servicio/{id}")]
        public async Task<IActionResult> DesactivarCatServicio(int id)
        {
            await _servicio.DesactivarCategoriaServicioAsync(id);
            return NoContent();
        }
    }
}