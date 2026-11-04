using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SALONBELLEZA.API.DTOs;
using SALONBELLEZA.CORE.Interfaces;

namespace SALONBELLEZA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalonController : ControllerBase
    {
        private readonly ISalonService _servicio; // 🔥 CAMBIO
        private readonly IMapper _mapper;

        public SalonController(ISalonService servicio, IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var salon = await _servicio.ObtenerSalonAsync();
            return salon == null ? NotFound() : Ok(_mapper.Map<SalonDto>(salon));
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarSalonRequest request)
        {
            await _servicio.ActualizarSalonAsync(1, request.Nombre ?? "", request.Direccion ?? "", request.Telefono ?? "", request.Email ?? "", request.Logo ?? "");
            return Ok(new { Mensaje = "Salón actualizado." });
        }

        [HttpGet("estado")]
        public IActionResult Estado() => Ok(new { sistema = "SALONBELLEZA", estado = "API funcionando", fecha = DateTime.Now });
    }
}