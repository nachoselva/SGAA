namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;

        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }



    }
}
