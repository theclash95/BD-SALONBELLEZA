using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaProductoService _servicioProducto; // 🔥 CAMBIO 1
        private readonly ICategoriaServicioService _servicioServicio; // 🔥 CAMBIO 2

        public CategoriasController(ICategoriaProductoService servicioProducto, ICategoriaServicioService servicioServicio)
        {
            _servicioProducto = servicioProducto;
            _servicioServicio = servicioServicio;
        }

        [HttpGet("producto")]
        public async Task<IActionResult> ObtenerCatProductos() => Ok(await _servicioProducto.ObtenerCategoriasProductoAsync());

        [HttpGet("servicio")]
        public async Task<IActionResult> ObtenerCatServicios() => Ok(await _servicioServicio.ObtenerCategoriasServicioAsync());

        [HttpPost("producto")]
        public async Task<IActionResult> CrearCatProducto([FromBody] CategoriaProductoRequest request)
        {
            int id = await _servicioProducto.RegistrarCategoriaProductoAsync(request.Nombre, request.Descripcion);
            return StatusCode(201, new { Mensaje = "Categoría de producto creada.", Id = id });
        }

        [HttpPost("servicio")]
        public async Task<IActionResult> CrearCatServicio([FromBody] CategoriaServicioRequest request)
        {
            int id = await _servicioServicio.RegistrarCategoriaServicioAsync(request.Nombre, request.Descripcion);
            return StatusCode(201, new { Mensaje = "Categoría de servicio creada.", Id = id });
        }

        [HttpDelete("producto/{id}")]
        public async Task<IActionResult> DesactivarCatProducto(int id)
        {
            await _servicioProducto.DesactivarCategoriaProductoAsync(id);
            return NoContent();
        }

        [HttpDelete("servicio/{id}")]
        public async Task<IActionResult> DesactivarCatServicio(int id)
        {
            await _servicioServicio.DesactivarCategoriaServicioAsync(id);
            return NoContent();
        }
    }
}