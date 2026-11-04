using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        // Inyectamos todos los servicios que necesitamos
        private readonly IClienteService _clienteService;
        private readonly IProductoService _productoService;
        private readonly IEmpleadoService _empleadoService;
        private readonly IVentaService _ventaService;

        public DashboardController(
            IClienteService clienteService,
            IProductoService productoService,
            IEmpleadoService empleadoService,
            IVentaService ventaService)
        {
            _clienteService = clienteService;
            _productoService = productoService;
            _empleadoService = empleadoService;
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodo()
        {
            // Llamamos a todos los servicios en paralelo para que sea rápido
            var clientesTask = _clienteService.ObtenerClientesAsync();
            var productosTask = _productoService.ObtenerProductosAsync();
            var empleadosTask = _empleadoService.ObtenerEmpleadosAsync();
            var ventasTask = _ventaService.ObtenerVentasAsync();

            await Task.WhenAll(clientesTask, productosTask, empleadosTask, ventasTask);

            // Armamos un solo JSON con todo
            return Ok(new
            {
                Clientes = await clientesTask,
                Productos = await productosTask,
                Empleados = await empleadosTask,
                Ventas = await ventasTask
            });
        }
    }
}