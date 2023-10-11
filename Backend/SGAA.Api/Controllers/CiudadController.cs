namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;

        public CiudadController(ICiudadService ciudadService)
        {
            _ciudadService = ciudadService;
        }


    }
}
