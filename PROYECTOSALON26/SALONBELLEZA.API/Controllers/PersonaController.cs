using Microsoft.AspNetCore.Mvc;

namespace SALONBELLEZA.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : Controller
    {
        [HttpGet]

        public string Get () {

            return "Hola mundo del IES";
    }
}
}

