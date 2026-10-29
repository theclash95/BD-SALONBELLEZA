using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Entidades;
using SALONBELLEZA.CORE.Servicios;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ServicioSalon _servicio;
        public ProductosController(ServicioSalon servicio) => _servicio = servicio;

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerProductosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var prod = await _servicio.ObtenerProductoPorIdAsync(id);
            return prod == null ? NotFound() : Ok(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] ProductoRequest request)
        {
            try
            {
                var producto = new Producto
                {
                    IdCategoriaProducto = request.IdCategoriaProducto,
                    Codigo = request.Codigo,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    PrecioCosto = request.PrecioCosto,
                    PrecioVenta = request.PrecioVenta,
                    StockActual = request.Stock,
                    StockMinimo = request.StockMinimo
                };
                int id = await _servicio.RegistrarProductoAsync(producto);
                return StatusCode(201, new { Mensaje = "Producto registrado.", Id = id });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoActualizarRequest request)
        {
            try
            {
                if (id != request.IdProducto) return BadRequest("El ID no coincide.");
                await _servicio.ActualizarProductoAsync(id, request.IdCategoriaProducto, request.Codigo, request.Nombre, request.Descripcion, request.PrecioCosto ?? 0, request.PrecioVenta, request.StockMinimo ?? 0);
                return Ok(new { Mensaje = "Producto actualizado." });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _servicio.DesactivarProductoAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> ActualizarStock(int id, [FromBody] StockRequest request)
        {
            try
            {
                await _servicio.ActualizarStockProductoAsync(id, request.Cantidad, request.TipoMovimiento, request.Motivo);
                return Ok(new { Mensaje = "Stock actualizado." });
            }
            catch (Exception ex) { return BadRequest(new { Mensaje = ex.Message }); }
        }
    }
}